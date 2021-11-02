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
using Microsoft.Extensions.Logging;

namespace CED.Data.Repositories
{
    public class UserRepository : DataProvider, IUserRepository
    {
        private readonly ILogger<UserRepository> _log;

        public UserRepository(
            ILogger<UserRepository> log,
            IOptions<ConnectionStrings> connectionStrings)
            : base(connectionStrings.Value.CEDDB)
        {
            _log = log;
        }

        public async Task<User> GetUserById(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateNewUser(RegistrationDTO registrationDTO)
        {
            _log.LogInformation("UserRepository: Start CreateNewUser : {Registration DTO}", registrationDTO);
            try
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
            catch (Exception e)
            {
                _log.LogCritical(e, "UserRepository ERROR: Exception occurred in (CreateNewUser) Registration : {registrationDTO}", registrationDTO);
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            _log.LogInformation("UserRepository: Start Get User By Email : {Email}", email);
            User result = null;
            try
            {
                using DataConnectionProvider dcp = CreateConnection();
                await using var command = dcp.CreateCommand("GetUserByEmail");

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Email", email);
                using DataReaderHelper drh = await command.ExecuteReaderAsync();

                while (drh.Read())
                    result = ReadUser(drh);
            }
            catch (Exception e)
            {
                _log.LogCritical(e, "UserRepository ERROR: Exception occurred in (GetUserByEmail) Email : {email}", email);
            }

            return result;
        }

        public async Task<User> GetUserByRefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            _log.LogInformation("UserRepository: Start Get User By Refresh Token : {Refresh Token DTO}", refreshTokenDTO);
            User result = null;
            try
            {
                using DataConnectionProvider dcp = CreateConnection();
                await using var command = dcp.CreateCommand("GetUserByRefreshToken");

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Token", refreshTokenDTO.Token);
                using DataReaderHelper drh = await command.ExecuteReaderAsync();

                while (drh.Read())
                    result = ReadUser(drh);
            }
            catch (Exception e)
            {
                _log.LogCritical(e, "UserRepository ERROR: Exception occurred in (GetUserByRefreshToken) Refresh Token : {refreshTokenDTO}", refreshTokenDTO);
            }
            
            return result;
        }

        public async Task Logout(string token)
        {
            _log.LogInformation("UserRepository: Start Logout : {Token}", token);
            try
            {
                string spName = "RevokeToken";
                using DataConnectionProvider dcp = CreateConnection();
                await using var command = dcp.CreateCommand(spName);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("token", token);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                _log.LogCritical(e, "UserRepository ERROR: Exception occurred in (Logout) Token : {token}", token);
            }
        }

        private User ReadUser(DataReaderHelper drh)
        {
            Guid guid;
            if (Guid.TryParse(drh.Get<string>("iduser"), out guid))
            {
                return new User()
                {
                    Id = guid,
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

            return null;
        }
    }
}
