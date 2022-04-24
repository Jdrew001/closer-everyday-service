﻿using CED.Models;
using CED.Models.Core;
using CED.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(Guid userId);
        Task CreateNewUser(RegistrationDTO registrationDTO);
        Task<User> ConfirmNewUser(string email);
        Task<User> GetUserByRefreshToken(RefreshTokenDTO refreshTokenDTO);

        Task<AuthCode> GetUserAuthCode(string email);
        Task<AuthCode> CreateUserAuthCode(Guid userId, string code);
        Task<AuthCode> DeleteUserAuthCode(string email);

        Task Logout(string token);
        Task<List<User>> SearchForUser(string param);
    }
}
