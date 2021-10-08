using CED.Models;
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
        Task CreateNewUser(RegistrationDTO registrationDTO);
        Task<User> GetUserByRefreshToken(RefreshTokenDTO refreshTokenDTO);
        Task Logout(string token);
    }
}
