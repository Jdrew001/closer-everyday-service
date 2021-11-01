﻿using System;
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

        public async Task<Schedule> GetScheduleByHabitId(Guid habitId)
        {
            Schedule schedule = null;
            string spName = "GetScheduleByHabitId";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("HabitId", habitId.ToString());

            using DataReaderHelper drh = await command.ExecuteReaderAsync();
            while (drh.Read())
                schedule = ReadSchedule(drh);

            return schedule;
        }

        // User will be able to add their schedule -- it will be four records (anytime, morning, afternoon, evening)
        public async Task<Schedule> SaveSchedule(Schedule schedule)
        {
            string spName = "SaveSchedule";
            Schedule newSchedule = null;
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("ScheduleTypeId", schedule.ScheduleType.Id);
            command.Parameters.AddWithValue("UserId", schedule.UserId.ToString());
            command.Parameters.AddWithValue("ScheduleTime", schedule.ScheduleTime);

            using DataReaderHelper drh = await command.ExecuteReaderAsync();
            while (drh.Read())
                newSchedule = ReadSchedule(drh);

            return newSchedule;
        }

        public async Task<Schedule> UpdateSchedule(Schedule schedule)
        {
            string spName = "UpdateSchedule";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Id", schedule.Id.ToString());
            command.Parameters.AddWithValue("ScheduleTypeId", schedule.ScheduleType.Id);
            command.Parameters.AddWithValue("UserId", schedule.UserId.ToString());
            command.Parameters.AddWithValue("ScheduleTime", schedule.ScheduleTime);

            using DataReaderHelper drh = await command.ExecuteReaderAsync();
            while (drh.Read())
            {
                var test = ReadSchedule(drh);
                schedule = test;
            }
                

            return schedule;
        }

        private Schedule ReadSchedule(DataReaderHelper drh)
        {
            return new Schedule()
            {
                Id = new Guid(drh.Get<string>("Id")),
                ScheduleTime = drh.Get<DateTime>("ScheduleTime"),
                ScheduleType = new ScheduleType()
                {
                    Id = drh.Get<int>("idschedule_type"),
                    Value = drh.Get<string>("scheduleType")
                },
                UserId = new Guid(drh.Get<string>("UserId"))
            };
        }
    }
}
