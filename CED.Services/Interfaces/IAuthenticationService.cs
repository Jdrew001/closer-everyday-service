using CED.Models.Core;
using CED.Models.DTO;
using System;
using System.Threading.Tasks;

namespace CED.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationDTO> Login(LoginRequestDTO loginRequestDto, string deviceUUID);
        Task<RegistrationUserDTO> Register(RegistrationDTO registrationDto);
        Task<AuthenticationDTO> RefreshToken(RefreshTokenDTO refreshTokenDto);
        Task Logout(string token);

        Task<AuthCodeDTO> GetAuthCode(Guid userId);
        Task<AuthCodeDTO> CreateUserAuthCode(Guid userId, string code);
        Task<AuthCodeDTO> DeleteUserAuthCode(Guid userId);
    }
}
