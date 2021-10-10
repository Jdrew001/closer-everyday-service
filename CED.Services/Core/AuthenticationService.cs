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
using CED.Data.Interfaces;

namespace CED.Services.Core
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDeviceService _deviceService;
        private readonly JwtToken _jwtToken;
        private readonly ILogger<AuthenticationService> _log;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthenticationService(
            ILogger<AuthenticationService> log,
            IOptions<JwtToken> jwtToken,
            IOptions<ConnectionStrings> connectionStrings,
            IDeviceService deviceService,
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _log = log;
            _jwtToken = jwtToken.Value;
            _deviceService = deviceService;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        #region authentication methods
        public async Task<AuthenticationDTO> Login(LoginRequestDTO loginRequestDto, string deviceUUID)
        {
            var user = await _userRepository.GetUserByEmail(loginRequestDto.Email);
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

            user.RefreshTokens = await _refreshTokenRepository.GetUserRefreshTokens(user.Id);
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
                await _refreshTokenRepository.SaveRefreshToken(refreshToken, user.Id);
            }

            return authenticationDTO;
        }

        public async Task<AuthenticationDTO> Register(RegistrationDTO registrationDto)
        {
            var user = await _userRepository.GetUserByEmail(registrationDto.email);

            // user found in db, unable to register error thrown
            if (user != null)
            {
                return RegistrationError();
            }

            await _userRepository.CreateNewUser(registrationDto);

            return await Login(new LoginRequestDTO()
            {
                Email = registrationDto.email,
                Password = registrationDto.password,
                IpAddress = registrationDto.IpAddress
            }, registrationDto.deviceGuid);
        }

        public async Task Logout(string token)
        {
            await _userRepository.Logout(token);
        }

        public async Task<AuthenticationDTO> RefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            var authenticationDTO = new AuthenticationDTO();
            if (string.IsNullOrEmpty(refreshTokenDTO.Token))
            {
                authenticationDTO.IsAuthenticated = false;
                authenticationDTO.ShouldRedirectoToLogin = true;
                authenticationDTO.Message = "Token invalid.";
                return authenticationDTO;
            }

            var user = await _userRepository.GetUserByRefreshToken(refreshTokenDTO);
            if (user == null)
            {
                authenticationDTO.IsAuthenticated = false;
                authenticationDTO.ShouldRedirectoToLogin = true;
                authenticationDTO.Message = "Token did not match any users.";
                return authenticationDTO;
            }

            var refreshToken = await _refreshTokenRepository.GetRefreshToken(refreshTokenDTO.Token);
            var devices = await _deviceService.GetUserDevices(user.Id);
            var userDevice = devices.Find(d => (d.UUID.Equals(refreshTokenDTO.DeviceUUID)));

            if (userDevice == null)
            {
                authenticationDTO.IsAuthenticated = false;
                authenticationDTO.ShouldRedirectoToLogin = true;
                authenticationDTO.Message = "Device not matching.";

                return authenticationDTO;
            }

            var delRefreshToken = await _refreshTokenRepository.DeleteRefreshToken(refreshToken.Token);
            if (delRefreshToken != null)
            {
                authenticationDTO.IsAuthenticated = false;
                authenticationDTO.ShouldRedirectoToLogin = true;
                authenticationDTO.Message = "Unable to process request";

                return authenticationDTO;
            }

            //create a new refresh token
            var newRefreshToken = await CreateRefreshToken(userDevice);
            user.RefreshTokens ??= new List<RefreshToken>();

            user.RefreshTokens.Add(newRefreshToken);

            await _refreshTokenRepository.SaveRefreshToken(refreshToken, user.Id);

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
        #endregion

        #region private util methods

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
        #endregion
    }
}
