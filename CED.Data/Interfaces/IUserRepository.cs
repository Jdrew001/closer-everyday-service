using CED.Models;
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
        Task<User> GetUserByRefreshToken(RefreshTokenDTO refreshTokenDTO);

        Task<AuthCode> GetUserAuthCode(Guid userId);
        Task<AuthCode> CreateUserAuthCode(Guid userId, string code);
        Task<AuthCode> DeleteUserAuthCode(Guid userId);

        Task Logout(string token);
        Task<List<User>> SearchForUser(string param);
    }
}
