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
    public class ScheduleRepository : DataProvider, IScheduleRepository
    {
        public ScheduleRepository(
            IOptions<ConnectionStrings> connectionStrings)
            : base(connectionStrings.Value.CEDDB)
        {
        }

        public async Task<Schedule> GetScheduleByHabitId(int habitId)
        {
            Schedule schedule = null;
            string spName = "SaveSchedule";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("HabitId", habitId);

            using DataReaderHelper drh = await command.ExecuteReaderAsync();
            while (drh.Read())
                schedule = ReadSchedule(drh);

            return schedule;
        }

        public async Task<Schedule> SaveSchedule(Schedule schedule)
        {
            string spName = "SaveSchedule";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("ScheduleTypeId", schedule.ScheduleType.Id);
            command.Parameters.AddWithValue("UserId", schedule.UserId);
            command.Parameters.AddWithValue("ScheduleTime", schedule.ScheduleTime);

            using DataReaderHelper drh = await command.ExecuteReaderAsync();
            while (drh.Read())
                schedule = ReadSchedule(drh);

            return schedule;
        }

        public async Task<Schedule> UpdateSchedule(Schedule schedule)
        {
            string spName = "UpdateSchedule";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Id", schedule.Id);
            command.Parameters.AddWithValue("ScheduleTypeId", schedule.ScheduleType.Id);
            command.Parameters.AddWithValue("UserId", schedule.UserId);
            command.Parameters.AddWithValue("ScheduleTime", schedule.ScheduleTime);

            using DataReaderHelper drh = await command.ExecuteReaderAsync();
            while (drh.Read())
                schedule = ReadSchedule(drh);

            return schedule;
        }

        private Schedule ReadSchedule(DataReaderHelper drh)
        {
            return new Schedule()
            {
                Id = drh.Get<int>("Id"),
                ScheduleTime = drh.Get<DateTime>("ScheduleTime"),
                ScheduleType = new ScheduleType()
                {
                    Id = drh.Get<int>("idschedule_type"),
                    Value = drh.Get<string>("scheduleType")
                },
                UserId = drh.Get<int>("UserId")
            };
        }
    }
}
