﻿using CED.Models.Core;
using CED.Models.DTO;
using System.Threading.Tasks;

namespace CED.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationDTO> Login(LoginRequestDTO loginRequestDto, string deviceUUID);
        Task<AuthenticationDTO> Register(RegistrationDTO registrationDto);
        Task<AuthenticationDTO> RefreshToken(RefreshTokenDTO refreshTokenDto);
        Task Logout(string token);
    }
}
