using CED.Models.Core;
using CED.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CED.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationDTO> Login(LoginRequestDTO loginRequestDto, string deviceUUID);
        Task<RegistrationUserDTO> Register(RegistrationDTO registrationDto);
        Task<bool> ResendValidationCode(string email);
        Task<AuthenticationDTO> ConfirmUser(string email, string deviceUUID, bool forReset);
        Task<AuthenticationDTO> RefreshToken(RefreshTokenDTO refreshTokenDto);
        Task<List<RefreshToken>> GetUserRefreshTokens(Guid userId);
        Task<bool> Logout(string appToken, string refreshToken);
        Task<AuthCodeDTO> GetAuthCode(string email);
        Task<AuthCodeDTO> CreateUserAuthCode(Guid userId, string code);
        Task<AuthCodeDTO> DeleteUserAuthCode(string email);
        Task<bool> SendValidationCode(string email, string code);
        Task<EmailForReset> EmailForReset(string email);
        Task<AuthenticationDTO> ResetPassword(Guid userId, string password);
    }
}
