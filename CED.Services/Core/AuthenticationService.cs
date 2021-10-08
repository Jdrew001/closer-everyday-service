using CED.Data;
using CED.Models;
using CED.Models.Core;
using CED.Models.DTO;
using CED.Models;
using CED.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Data;

namespace CED.Services.Core
{
    public class AuthenticationService : DataProvider, IAuthenticationService
    {

        private readonly IUserService _userService;
        private readonly IDeviceService _deviceService;
        private readonly JwtToken _jwtToken;
        private readonly ILogger<AuthenticationService> _log;

        public AuthenticationService(
            ILogger<AuthenticationService> log,
            IOptions<JwtToken> jwtToken,
            IOptions<ConnectionStrings> connectionStrings,
            IDeviceService deviceService)
            : base(connectionStrings.Value.CEDDB)
        {
            _log = log;
            _jwtToken = jwtToken.Value;
            _deviceService = deviceService;
        }
        
        public async Task<AuthenticationDTO> Login(LoginRequestDTO loginRequestDto, string deviceUUID)
        {
            var user = await GetUserByEmail(loginRequestDto.Email);
            var authenticationDTO = new AuthenticationDTO();
            if (user == null)
            {
                return RegistrationError();
            }

            if (Hash.GetHash(loginRequestDto.Password, user.PasswordSalt).Hash != user.Password)
            {
                return RegistrationError();
            }

            var devices = await _deviceService.GetUserDevices(user.Id);
            var userDevice = devices.Find(d => (d.UUID.Equals(deviceUUID)));
            if (userDevice == null)
            {
                // TODO: Send an email to user asking if the phone logging in is correct
                // if click on ALLOW, then call an end point updating the system
                // eventually I want to use texting 
                return AddNewDevice();
            }

            user.RefreshTokens = await GetUserRefreshTokens(user.Id);
            var activeRefreshToken = user.RefreshTokens.FirstOrDefault(a => a.IsActive);

            // users request device uuid in use doesn't match the refresh token -- throw an error
            if (activeRefreshToken != null && activeRefreshToken.DeviceId != userDevice.DeviceId)
            {
                return DeviceNotRecognized();
            }

            authenticationDTO.IsAuthenticated = true;
            authenticationDTO.Token = await CreateJwtToken(user);
            authenticationDTO.UserId = user.Id;

            if (activeRefreshToken != null)
            {
                authenticationDTO.RefreshToken = activeRefreshToken.Token;
                authenticationDTO.RefreshTokenExpiration = activeRefreshToken.Expires;
            }
            else
            {
                var refreshToken = await CreateRefreshToken(userDevice);
                authenticationDTO.RefreshToken = refreshToken.Token;
                authenticationDTO.RefreshTokenExpiration = refreshToken.Expires;
                user.RefreshTokens ??= new List<RefreshToken>();
                user.RefreshTokens.Add(refreshToken);
                await SaveRefreshToken(refreshToken, user.Id);
            }

            return authenticationDTO;
        }

        public async Task<AuthenticationDTO> Register(RegistrationDTO registrationDto)
        {
            var user = await GetUserByEmail(registrationDto.email);

            // user found in db, unable to register error thrown
            if (user != null)
            {
                return RegistrationError();
            }

            await CreateNewUser(registrationDto);

            return await Login(new LoginRequestDTO()
            {
                Email = registrationDto.email,
                Password = registrationDto.password,
                IpAddress = registrationDto.IpAddress
            }, registrationDto.deviceGuid);
        }

        public async Task Logout(string token)
        {
            string spName = "RevokeToken";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("token", token);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<AuthenticationDTO> RefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            var authenticationDTO = new AuthenticationDTO();
            if (string.IsNullOrEmpty(refreshTokenDTO.Token))
            {
                authenticationDTO.IsAuthenticated = false;
                authenticationDTO.Message = "Token invalid.";
                return authenticationDTO;
            }

            var user = await GetUserByRefreshToken(refreshTokenDTO);
            if (user == null)
            {
                authenticationDTO.IsAuthenticated = false;
                authenticationDTO.Message = "Token did not match any users.";
                return authenticationDTO;
            }

            var refreshToken = await GetRefreshToken(refreshTokenDTO.Token);
            var devices = await _deviceService.GetUserDevices(user.Id);
            var userDevice = devices.Find(d => (d.UUID.Equals(refreshTokenDTO.DeviceUUID)));
            if (!refreshToken.IsActive)
            {
                authenticationDTO.IsAuthenticated = false;
                authenticationDTO.Message = "Token not active.";
                return authenticationDTO;
            }

            if (userDevice == null)
            {
                authenticationDTO.IsAuthenticated = false;
                authenticationDTO.Message = "Device did not match.";
                return authenticationDTO;
            }

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.isRevoked = true;

            var newRefreshToken = await CreateRefreshToken(userDevice);
            user.RefreshTokens ??= new List<RefreshToken>();

            user.RefreshTokens.Add(newRefreshToken);

            await SaveRefreshToken(newRefreshToken, user.Id);

            //Generates new jwt
            authenticationDTO.IsAuthenticated = true;
            authenticationDTO.Token = await CreateJwtToken(user);
            authenticationDTO.UserId = user.Id;
            authenticationDTO.RefreshToken = newRefreshToken.Token;
            authenticationDTO.RefreshTokenExpiration = newRefreshToken.Expires;
            return authenticationDTO;
        }

        public async Task<string> CreateJwtToken(User user)
        {
            return await Task.Run(() =>
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtToken.SecretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("uemail", user.Email),
                    new Claim("uid", user.Id.ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: _jwtToken.Issuer,
                    audience: _jwtToken.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtToken.TokenExpiry)),
                    notBefore: DateTime.Now.Subtract(TimeSpan.FromMinutes(30)),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            });
        }
        public async Task<RefreshToken> CreateRefreshToken(Device device)
        {
            return await Task.Run(() =>
            {
                string token;
                var randomNumber = new byte[32];
                //todo: add ipaddress validation
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomNumber);
                    token = Convert.ToBase64String(randomNumber);
                }

                // Create the refresh token
                RefreshToken refreshToken = new RefreshToken()
                {
                    Token = token,
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtToken.RefreshTokenExpiry)),
                    Created = DateTime.UtcNow,
                    Revoked = null,
                    isRevoked = false,
                    DeviceId = device.DeviceId
                };
                return refreshToken;
            });
        }

        #region private util methods
        private async Task<User> GetUserByEmail(string email)
        {
            User result = null;
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand("GetUserByEmail");

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Email", email);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                result = ReadUser(drh);

            return result;
        }
        private async Task SaveRefreshToken(RefreshToken token, int userId)
        {
            string spName = "SaveRefreshToken";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UserId", userId);
            command.Parameters.AddWithValue("Token", token.Token);
            command.Parameters.AddWithValue("IsExpired", token.IsExpired);
            command.Parameters.AddWithValue("Expires", token.Expires.ToUniversalTime());
            command.Parameters.AddWithValue("Created", token.Created.ToUniversalTime());
            command.Parameters.AddWithValue("Revoked", token.Revoked?.ToUniversalTime());
            command.Parameters.AddWithValue("IsRevoked", token.isRevoked);
            command.Parameters.AddWithValue("DeviceId", token.DeviceId);
            await command.ExecuteNonQueryAsync();
        }

        private async Task<List<RefreshToken>> GetUserRefreshTokens(int userId)
        {
            List<RefreshToken> result = new List<RefreshToken>();
            string spName = "GetUserRefreshTokenById";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UserId", userId);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                result.Add(ReadRefreshToken(drh));

            return result;
        }

        private async Task CreateNewUser(RegistrationDTO registrationDto)
        {
            var hashAndSalt = Hash.GetHash(registrationDto.password);
            string spName = "RegisterAccount";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Firstname", registrationDto.firstName);
            command.Parameters.AddWithValue("Lastname", registrationDto.lastName);
            command.Parameters.AddWithValue("Email", registrationDto.email);
            command.Parameters.AddWithValue("UserHash", hashAndSalt.Hash);
            command.Parameters.AddWithValue("Salt", hashAndSalt.Salt);
            command.Parameters.AddWithValue("DeviceGUID", registrationDto.deviceGuid);
            command.Parameters.AddWithValue("DeviceModel", registrationDto.deviceModel);
            command.Parameters.AddWithValue("DevicePlatform", registrationDto.devicePlatform);
            command.Parameters.AddWithValue("Manufacturer", registrationDto.deviceManufacture);
            await command.ExecuteNonQueryAsync();
        }

        private async Task<User> GetUserByRefreshToken(RefreshTokenDTO dto)
        {
            User result = null;
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand("GetUserByRefreshToken");

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Token", dto.Token);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                result = ReadUser(drh);

            return result;
        }

        private async Task<RefreshToken> GetRefreshToken(string token)
        {
            RefreshToken result = null;
            string spName = "GetRefreshToken";

            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Token", token);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
            {
                result = new RefreshToken
                {
                    Token = drh.Get<string>("Token"),
                    Expires = drh.Get<DateTime>("Expires"),
                    Created = drh.Get<DateTime>("Created"),
                    Revoked = drh.Get<DateTime?>("Revoked"),
                    isRevoked = drh.Get<bool>("is_revoked"),
                    DeviceId = drh.Get<int>("deviceId")
                };
            }

            return result;
        }

        private AuthenticationDTO UserNotFound(string email)
        {
            var authenticationDTO = new AuthenticationDTO();
            authenticationDTO.IsAuthenticated = false;
            authenticationDTO.Message = $"No Accounts Registered with {email}";
            authenticationDTO.IsNewDevice = false;
            return authenticationDTO;
        }

        private AuthenticationDTO DeviceNotRecognized()
        {
            var authenticationDTO = new AuthenticationDTO();
            authenticationDTO.IsAuthenticated = false;
            authenticationDTO.Message = $"Device not Authorized";
            authenticationDTO.IsNewDevice = false;
            return authenticationDTO;
        }

        private AuthenticationDTO AddNewDevice()
        {
            var authenticationDTO = new AuthenticationDTO();
            authenticationDTO.IsAuthenticated = false;
            authenticationDTO.Message = $"New Device Detected";
            authenticationDTO.IsNewDevice = true;
            return authenticationDTO;
        }

        private AuthenticationDTO RegistrationError()
        {
            var authenticationDTO = new AuthenticationDTO();
            authenticationDTO.IsAuthenticated = false;
            authenticationDTO.Message = $"Email or password incorrect";
            authenticationDTO.IsNewDevice = false;
            return authenticationDTO;
        }
        private User ReadUser(DataReaderHelper drh)
        {
            return new User()
            {
                Id = drh.Get<int>("iduser"),
                Email = drh.Get<string>("email"),
                Username = drh.Get<string>("username"),
                FirstName = drh.Get<string>("firstname"),
                LastName = drh.Get<string>("lastname"),
                LastLogin = drh.Get<DateTime?>("lastLogin"),
                Locked = drh.Get<bool>("locked"),
                DateLocked = drh.Get<DateTime?>("datelocked"),
                PasswordSalt = drh.Get<string>("passwordSalt"),
                Password = drh.Get<string>("password")
            };
        }

        private RefreshToken ReadRefreshToken(DataReaderHelper drh)
        {
            return new RefreshToken()
            {
                Token = drh.Get<string>("token"),
                Expires = drh.Get<DateTime>("expires"),
                Created = drh.Get<DateTime>("created"),
                Revoked = drh.Get<DateTime>("revoked"),
                DeviceId = drh.Get<int>("deviceId")
            };
        }
        #endregion
    }
}
