using System;
using CED.Data.Interfaces;
using CED.Models;
using Microsoft.Extensions.Options;
using System.Data;
using System.Threading.Tasks;

namespace CED.Data.Repositories
{
    public class HabitStatRepository : DataProvider, IHabitStatRepository
    {
        public HabitStatRepository(
            IOptions<ConnectionStrings> connectionStrings)
            : base(connectionStrings.Value.CEDDB)
        {
        }

        public async Task<double> GetGlobalSuccessRate(Guid userId)
        {
            double rate = 0;
            string spName = "GetAvgSuccessLogsForUser";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UserId", userId);

            using DataReaderHelper drh = await command.ExecuteReaderAsync();
            while (drh.Read())
                rate = drh.Get<double>("COMPLETED_PERCENTAGE");

            return rate;
        }

        public async Task<int> GetFriendStat(Guid userId)
        {
            int count = 0;
            string spName = "GetAvgSuccessLogsForUser";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UserId", userId);

            using DataReaderHelper drh = await command.ExecuteReaderAsync();
            while (drh.Read())
                count = drh.Get<int>("FRIEND_STAT");

            return count;
        }
    }
}
