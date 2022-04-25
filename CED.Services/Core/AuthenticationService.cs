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
using AutoMapper;
using CED.Services.utils;
using MimeKit;

namespace CED.Services.Core
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDeviceService _deviceService;
        private readonly JwtToken _jwtToken;
        private readonly ILogger<AuthenticationService> _log;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;

        private readonly IMapper _mapper;

        public AuthenticationService(
            ILogger<AuthenticationService> log,
            IOptions<JwtToken> jwtToken,
            IOptions<ConnectionStrings> connectionStrings,
            IDeviceService deviceService,
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IEmailService emailService,
            IEmailTemplateService emailTemplateService,
            IMapper mapper)
        {
            _log = log;
            _jwtToken = jwtToken.Value;
            _deviceService = deviceService;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _mapper = mapper;
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
        }

        #region authentication methods
        public async Task<AuthenticationDTO> Login(LoginRequestDTO loginRequestDto, string deviceUUID)
        {
            var user = await _userRepository.GetUserByEmail(loginRequestDto.Email);
            var authenticationDTO = new AuthenticationDTO();
            if (user == null)
            {
                return AuthenticationError();
            }

            if (Hash.GetHash(loginRequestDto.Password, user.PasswordSalt).Hash != user.Password)
            {
                return AuthenticationError();
            }

            var devices = await _deviceService.GetUserDevices(user.Id);
            var userDevice = devices.Find(d => (d.UUID.Equals(deviceUUID)));
            if (userDevice == null)
            {
                // TODO: Send an email to user asking if the phone logging in is correct
                // if click on ALLOW, then call an end point updating the system
                // eventually I want to use texting 
                //_log.LogError(e, "test: {devices}", devices);

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

        public async Task<RegistrationUserDTO> Register(RegistrationDTO registrationDto)
        {
            var user = await _userRepository.GetUserByEmail(registrationDto.email);

            // user found in db, unable to register error thrown
            if (user != null)
            {
                return RegistrationError();
            }

            await _userRepository.CreateNewUser(registrationDto);
            var loginUser = await _userRepository.GetUserByEmail(registrationDto.email);
            
            if (loginUser == null)
                return RegistrationError();

            // create a new code in the database for this user
            var authCodeDTO = await CreateUserAuthCode(loginUser.Id, Utils.GenerateRandomNo().ToString());

            if (authCodeDTO == null)
                return RegistrationError();

            if (!(await SendValidationCode(loginUser.Email, authCodeDTO.Code)))
                return RegistrationError();

            return new RegistrationUserDTO()
            {
                IsUserCreated = true,
                Message = null,
                ShouldRedirectToLogin = false,
                UserId = loginUser.Id
            };
        }

        public async Task<bool> SendValidationCode(string email, string code)
        {
            try 
            {
                // send auth code to user email
                var to = new List<MailboxAddress>() { new MailboxAddress(email, email) };
                var template = await _emailTemplateService.RegisterCode(email, code);
                await _emailService.SendEmailTemplate(to, "Verify Account", template.ToMessageBody());
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<AuthenticationDTO> ConfirmUser(string email, string deviceUUID)
        {
            var authenticationDTO = new AuthenticationDTO();
            try 
            {
                var confirmedUser = await _userRepository.ConfirmNewUser(email);

                if (confirmedUser == null)
                    return AuthenticationError();

                if (!confirmedUser.Confirmed)
                    return AuthenticationError();

                var devices = await _deviceService.GetUserDevices(confirmedUser.Id);
                var userDevice = devices.Find(d => (d.UUID.Equals(deviceUUID)));

                if (userDevice == null)
                {
                    // TODO: Send an email to user asking if the phone logging in is correct
                    // if click on ALLOW, then call an end point updating the system
                    // eventually I want to use texting 
                    //_log.LogError(e, "test: {devices}", devices);

                    return AddNewDevice();
                }
                
                
                authenticationDTO.IsAuthenticated = true;
                authenticationDTO.Token = await CreateJwtToken(confirmedUser);
                authenticationDTO.UserId = confirmedUser.Id;

                var refreshToken = await CreateRefreshToken(userDevice);
                authenticationDTO.RefreshToken = refreshToken.Token;
                authenticationDTO.RefreshTokenExpiration = refreshToken.Expires;
                confirmedUser.RefreshTokens ??= new List<RefreshToken>();
                confirmedUser.RefreshTokens.Add(refreshToken);
                await _refreshTokenRepository.SaveRefreshToken(refreshToken, confirmedUser.Id);
            }
            catch(Exception e)
            {
                return AuthenticationError();
            }
            
            return authenticationDTO;
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

            await _refreshTokenRepository.SaveRefreshToken(newRefreshToken, user.Id);

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

        public async Task<AuthCodeDTO> GetAuthCode(string email)
        {
            AuthCode code = await _userRepository.GetUserAuthCode(email);
            return _mapper.Map<AuthCode, AuthCodeDTO>(code);
        }

        public async Task<AuthCodeDTO> CreateUserAuthCode(Guid userId, string code)
        {
            AuthCode authCode = await _userRepository.CreateUserAuthCode(userId, code);
            return _mapper.Map<AuthCode, AuthCodeDTO>(authCode);
        }

        public async Task<AuthCodeDTO> DeleteUserAuthCode(string email)
        {
            AuthCode authCode = await _userRepository.DeleteUserAuthCode(email);
            return _mapper.Map<AuthCode, AuthCodeDTO>(authCode);
        }

        public async Task<bool> ResendValidationCode(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
                return false;

            var deletionCode = await DeleteUserAuthCode(user.Email);
            if (deletionCode != null)
                return false;

            var authCodeDTO = await CreateUserAuthCode(user.Id, Utils.GenerateRandomNo().ToString());

            if (!(await SendValidationCode(email, authCodeDTO.Code)))
                return false;

            return true;
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

        private AuthenticationDTO AuthenticationError()
        {
            var authenticationDTO = new AuthenticationDTO();
            authenticationDTO.IsAuthenticated = false;
            authenticationDTO.Message = $"Email or password incorrect";
            authenticationDTO.IsNewDevice = false;
            return authenticationDTO;
        }

        private RegistrationUserDTO RegistrationError()
        {
            var registrationDTO = new RegistrationUserDTO() 
            {
                IsUserCreated = false,
                Message = "Unable to register user",
                ShouldRedirectToLogin = true,
                UserId = Guid.Empty
            };

            return registrationDTO;
        }
    #endregion
  }
}
