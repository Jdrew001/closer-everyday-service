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
        private readonly JwtToken _jwtToken;
        private readonly ILogger<AuthenticationService> _log;

        public AuthenticationService(
            ILogger<AuthenticationService> log,
            IOptions<JwtToken> jwtToken,
            IOptions<ConnectionStrings> connectionStrings)
            : base(connectionStrings.Value.CEDDB)
        {
            _log = log;
            _jwtToken = jwtToken.Value;
        }
        
        public async Task<AuthenticationDTO> Login(LoginRequestDTO loginRequestDto)
        {
            var user = await GetUserByEmail(loginRequestDto.Email);
            var userRefreshTokens = await GetUserRefreshTokens(user.Id);
            var authenticationDTO = new AuthenticationDTO();
            if (user == null)
            {
                return UserNotFound(loginRequestDto.Email);
            }

            if (Hash.GetHash(loginRequestDto.Password, user.PasswordSalt).Hash != user.Password)
            {
                return UserPasswordIncorrect(loginRequestDto);
            }

            if (userRefreshTokens != null)
            {
                user.RefreshTokens = userRefreshTokens;
            }

            authenticationDTO.IsAuthenticated = true;
            authenticationDTO.Token = await CreateJwtToken(user);
            authenticationDTO.UserId = user.Id;

            if (user.RefreshTokens != null && user.RefreshTokens.Any(a => a.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(a => a.IsActive);
                authenticationDTO.RefreshToken = activeRefreshToken.Token;
                authenticationDTO.RefreshTokenExpiration = activeRefreshToken.Expires;
            }
            else
            {
                var refreshToken = await CreateRefreshToken();
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

            await command.ExecuteNonQueryAsync();

            return await Login(new LoginRequestDTO()
            {
                Email = registrationDto.email,
                Password = registrationDto.password,
                IpAddress = registrationDto.IpAddress
            });
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

        public Task<AuthenticationDTO> RefreshToken(string token)
        {
            throw new NotImplementedException();
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
        public async Task<RefreshToken> CreateRefreshToken()
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
                    isRevoked = false
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
        private async Task SaveRefreshToken(RefreshToken token, Int32 userId)
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
            await command.ExecuteNonQueryAsync();
        }

        private async Task<List<RefreshToken>> GetUserRefreshTokens(Int32 userId)
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

        private AuthenticationDTO UserNotFound(string email)
        {
            var authenticationDTO = new AuthenticationDTO();
            authenticationDTO.IsAuthenticated = false;
            authenticationDTO.Message = $"No Accounts Registered with {email}";
            return authenticationDTO;
        }

        private AuthenticationDTO RegistrationError()
        {
            var authenticationDTO = new AuthenticationDTO();
            authenticationDTO.IsAuthenticated = false;
            authenticationDTO.Message = $"Registration attempt failed";
            return authenticationDTO;
        }

        private AuthenticationDTO UserPasswordIncorrect(LoginRequestDTO loginRequestDTO)
        {
            var authenticationDTO = new AuthenticationDTO();
            authenticationDTO.IsAuthenticated = false;
            authenticationDTO.Message = $"Incorrect Credentials for user {loginRequestDTO.Email}.";
            return authenticationDTO;
        }
        private User ReadUser(DataReaderHelper drh)
        {
            return new User()
            {
                Id = drh.Get<Int32>("iduser"),
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
                Revoked = drh.Get<DateTime>("revoked")
            };
        }
        #endregion
    }
}
