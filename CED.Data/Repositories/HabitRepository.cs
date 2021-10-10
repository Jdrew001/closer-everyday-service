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
    public class HabitRepository : DataProvider, IHabitRepository
    {
        private readonly IFrequencyRepository _frequencyRepository;

        public HabitRepository(
            IOptions<ConnectionStrings> connectionStrings,
            IFrequencyRepository frequencyRepository)
            : base(connectionStrings.Value.CEDDB)
        {
            _frequencyRepository = frequencyRepository;
        }

        public async Task<List<Habit>> GetAllHabits()
        {
            //GetAllHabits
            List<Habit> habits = new List<Habit>();
            string spName = "GetAllHabits";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;

            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
            {
                var habit = ReadHabit(drh);
                habit.Frequencies = await _frequencyRepository.GetHabitFrequencies(habit.Id);
                habits.Add(habit);
            }

            return habits;
        }
        public async Task<List<Habit>> GetAllUserHabits(int userId)
        {
            //GetAllUserHabits
            List<Habit> habits = new List<Habit>();
            string spName = "GetAlluserHabits";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("UserId", userId);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
            {
                var habit = ReadHabit(drh);
                habit.Frequencies = await _frequencyRepository.GetHabitFrequencies(habit.Id);
                habits.Add(habit);
            }

            return habits;
        }

        public async Task<Habit> GetHabitById(int id)
        {
            //GetHabitById
            Habit habit = null;
            string spName = "GetHabitById";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("HabitId", id);
            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while (drh.Read())
            {
                habit = ReadHabit(drh);
                habit.Frequencies = await _frequencyRepository.GetHabitFrequencies(habit.Id);
            }

            return habit;
        }

        public Task<Habit> SaveHabit(Habit habit)
        {
            //CreateHabit
            throw new System.NotImplementedException();
        }

        public Task<Habit> UpdateHabit(Habit habit)
        {
            // UpdateHabit
            throw new System.NotImplementedException();
        }
        public bool DeleteHabitById(int id)
        {
            throw new System.NotImplementedException();
        }

        private Habit ReadHabit(DataReaderHelper drh)
        {
            return new Habit()
            {
                Id = drh.Get<int>("idhabit"),
                Name = drh.Get<string>("name"),
                Icon = drh.Get<byte[]>("icon"),
                Reminder = drh.Get<bool>("reminder"),
                ReminderAt = drh.Get<DateTime>("reminderAt"),
                VisibleToFriends = drh.Get<bool>("visibleToFriends"),
                Description = drh.Get<string>("description"),
                Status = drh.Get<string>("status"),
                HabitType = new HabitType()
                {
                    Id = drh.Get<int>("habitTypeId"),
                    Value = drh.Get<string>("habitType"),
                    Description = drh.Get<string>("description")
                },
                Schedule = new Schedule()
                {
                    Id = drh.Get<int>("idSchedule"),
                    ScheduleTime = drh.Get<DateTime>("schedule_time"),
                    ScheduleType = new ScheduleType()
                    {
                        Id = drh.Get<int>("idschedule_type"),
                        Value = drh.Get<string>("scheduleType")
                    }
                },
                UserId = drh.Get<int>("userId")
            };
        }
    }
}
