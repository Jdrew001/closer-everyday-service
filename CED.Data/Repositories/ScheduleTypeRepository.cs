using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Data.Interfaces;
using CED.Models;
using CED.Models.Core;
using Microsoft.Extensions.Options;

namespace CED.Data.Repositories
{
    public class ScheduleTypeRepository : DataProvider, IScheduleTypeRepository
    {
        public ScheduleTypeRepository(
            IOptions<ConnectionStrings> connectionStrings)
            : base(connectionStrings.Value.CEDDB)
        {
        }

        public async Task<List<ScheduleType>> GetScheduleTypes()
        {
            List<ScheduleType> scheduleTypes = new List<ScheduleType>();
            string spName = "GetAllScheduleTypes";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;

            using DataReaderHelper drh = await command.ExecuteReaderAsync();
            while (drh.Read())
                scheduleTypes.Add(ReadScheduleType(drh));

            return scheduleTypes;
        }

        private ScheduleType ReadScheduleType(DataReaderHelper drh)
        {
            return new ScheduleType()
            {
                Id = drh.Get<int>("id"),
                Value = drh.Get<string>("Value")
            };
        }
    }
}
