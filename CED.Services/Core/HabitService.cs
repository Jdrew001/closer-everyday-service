using System;
using CED.Data.Interfaces;
using CED.Models.Core;
using CED.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CED.Services.Core
{
    public class HabitService : IHabitService
    {

        private readonly IHabitRepository _habitRepository;

        public HabitService(
            IHabitRepository habitRepository)
        {
            _habitRepository = habitRepository;
        }

        public Task<List<Habit>> GetAllHabits()
        {
            return _habitRepository.GetAllHabits();
        }

        public Task<List<Habit>> GetAllUserHabits(int userId)
        {
            return _habitRepository.GetAllUserHabits(userId);
        }

        public Task<Habit> GetHabitById(int id)
        {
            return _habitRepository.GetHabitById(id);
        }

        public Task<HabitLog> GetHabitLogByIdCurrentDate(int id)
        {
            return _habitRepository.GetHabitLogByIdAndCurrentDate(id);
        }

        public Task<HabitLog> GetHabitLog(int id)
        {
            return _habitRepository.GetHabitLogById(id);
        }

        public bool MarkHabitInactive(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Habit> SaveHabit(Habit habit)
        {
            return _habitRepository.SaveHabit(habit);
        }

        public Task<Habit> UpdateHabit(Habit habit)
        {
            return _habitRepository.UpdateHabit(habit);
        }

        public async Task<HabitLog> SaveHabitLog(char status, int userId, int habitId)
        {
            if (!status.Equals('C') && !status.Equals('F') && !status.Equals('P'))
                return null;

            // Habit log not null, then update otherwise create a new one
            var habit = await GetHabitById(habitId);

            if (habit == null)
                return null;

            var habitLog = await GetHabitLogByIdCurrentDate(habitId);
            if (habitLog == null)
                return await _habitRepository.SaveHabitLog(status, userId, habitId);
            else
                return await _habitRepository.UpdateHabitLog(status, habitId);
        }
    }
}
