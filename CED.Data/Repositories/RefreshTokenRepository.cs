using CED.Data.Interfaces;
using CED.Models;
using CED.Models.Core;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CED.Data.Repositories
{
    public class RefreshTokenRepository : DataProvider, IRefreshTokenRepository
    {
        public RefreshTokenRepository(
            IOptions<ConnectionStrings> connectionStrings)
            : base(connectionStrings.Value.CEDDB)
        {

        }
        public async Task<RefreshToken> GetRefreshToken(string token)
        {
            RefreshToken result = null;
            string spName = "GetRefreshToken";

            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Token", token);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
            {
                result = new RefreshToken
                {
                    Token = drh.Get<string>("Token"),
                    Expires = drh.Get<DateTime>("Expires"),
                    Created = drh.Get<DateTime>("Created"),
                    Revoked = drh.Get<DateTime?>("Revoked"),
                    isRevoked = drh.Get<bool>("is_revoked"),
                    DeviceId = drh.Get<Guid>("deviceId")
                };
            }

            return result;
        }

        public async Task<List<RefreshToken>> GetUserRefreshTokens(Guid userId)
        {
            List<RefreshToken> result = new List<RefreshToken>();
            string spName = "GetUserRefreshTokenById";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UserId", userId);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                result.Add(ReadRefreshToken(drh));

            return result;
        }

        public async Task SaveRefreshToken(RefreshToken token, Guid userId)
        {
            string spName = "SaveRefreshToken";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UserId", userId.ToString());
            command.Parameters.AddWithValue("Token", token.Token);
            command.Parameters.AddWithValue("IsExpired", token.IsExpired);
            command.Parameters.AddWithValue("Expires", token.Expires.ToUniversalTime());
            command.Parameters.AddWithValue("Created", token.Created.ToUniversalTime());
            command.Parameters.AddWithValue("Revoked", token.Revoked?.ToUniversalTime());
            command.Parameters.AddWithValue("IsRevoked", token.isRevoked);
            command.Parameters.AddWithValue("DeviceId", token.DeviceId.ToString());
            await command.ExecuteNonQueryAsync();
        }

        public async Task<RefreshToken> DeleteRefreshToken(string token)
        {
            RefreshToken reToken = null;
            string spName = "DeleteRefreshToken";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Token", token);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
                reToken = ReadRefreshToken(drh);

            return reToken;
        }

        private RefreshToken ReadRefreshToken(DataReaderHelper drh)
        {
            return new RefreshToken()
            {
                Token = drh.Get<string>("token"),
                Expires = drh.Get<DateTime>("expires"),
                Created = drh.Get<DateTime>("created"),
                Revoked = drh.Get<DateTime>("revoked"),
                DeviceId = drh.Get<Guid>("deviceId")
            };
        }
    }
}
