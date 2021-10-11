using CED.Data.Interfaces;
using CED.Models;
using CED.Models.Core;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Data.Repositories
{
    public class ReferenceRepository : DataProvider, IReferenceRepository
    {
        public ReferenceRepository(
            IOptions<ConnectionStrings> connectionStrings)
            : base(connectionStrings.Value.CEDDB)
        {
        }
        public async Task<List<HabitType>> GetHabitTypes()
        {
            List<HabitType> habitTypes = new List<HabitType>();
            string spName = "GetAllHabitTypes";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
            {
                habitTypes.Add(ReadHabitType(drh));
            }

            return habitTypes;
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
            {
                scheduleTypes.Add(ReadScheduleType(drh));
            }

            return scheduleTypes;
        }

        private HabitType ReadHabitType(DataReaderHelper drh)
        {
            return new HabitType()
            {
                Id = drh.Get<int>("habitTypeId"),
                Value = drh.Get<string>("value"),
                Description = drh.Get<string>("description")
            };
        }

        private ScheduleType ReadScheduleType(DataReaderHelper drh)
        {
            return new ScheduleType()
            {
                Id = drh.Get<int>("idschedule_type"),
                Value = drh.Get<string>("value")
            };
        }
    }
}
