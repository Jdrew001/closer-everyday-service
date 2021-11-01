using CED.Data.Interfaces;
using CED.Models;
using CED.Models.DTO;
using CED.Security;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Data.Repositories
{
    public class UserRepository : DataProvider, IUserRepository
    {
        public UserRepository(
            IOptions<ConnectionStrings> connectionStrings)
            : base(connectionStrings.Value.CEDDB)
        {
        }

        public async Task<User> GetUserById(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateNewUser(RegistrationDTO registrationDTO)
        {
            var hashAndSalt = Hash.GetHash(registrationDTO.password);
            string spName = "RegisterAccount";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Firstname", registrationDTO.firstName);
            command.Parameters.AddWithValue("Lastname", registrationDTO.lastName);
            command.Parameters.AddWithValue("Email", registrationDTO.email);
            command.Parameters.AddWithValue("UserHash", hashAndSalt.Hash);
            command.Parameters.AddWithValue("Salt", hashAndSalt.Salt);
            command.Parameters.AddWithValue("DeviceGUID", registrationDTO.deviceGuid);
            command.Parameters.AddWithValue("DeviceModel", registrationDTO.deviceModel);
            command.Parameters.AddWithValue("DevicePlatform", registrationDTO.devicePlatform);
            command.Parameters.AddWithValue("Manufacturer", registrationDTO.deviceManufacture);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<User> GetUserByEmail(string email)
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

        public async Task<User> GetUserByRefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            User result = null;
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand("GetUserByRefreshToken");

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Token", refreshTokenDTO.Token);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                result = ReadUser(drh);

            return result;
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

        private User ReadUser(DataReaderHelper drh)
        {
            return new User()
            {
                Id = new Guid(drh.Get<string>("iduser")),
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
    }
}
