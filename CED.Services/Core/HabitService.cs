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

        public Task<List<Habit>> GetAllUserHabits(int userId, string date)
        {
            return _habitRepository.GetAllUserHabits(userId, date);
        }

        public Task<Habit> GetHabitById(int id)
        {
            return _habitRepository.GetHabitById(id);
        }

        public Task<HabitLog> GetHabitLogByIdDate(int id, string date)
        {
            return _habitRepository.GetHabitLogByIdAndDate(id, date);
        }

        public Task<HabitLog> GetHabitLog(int id)
        {
            return _habitRepository.GetHabitLogById(id);
        }

        public Task<List<HabitLog>> GetAllCompletedLogsForUser(int userId)
        {
            return _habitRepository.GetAllCompletedLogsForUser(userId);
        }

        public async Task<List<HabitLog>> GetUserHabitLogs(int userId)
        {
            return await _habitRepository.GetAllLogsForUser(userId);
        }

        public bool MarkHabitInactive(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<HabitLog>> GetLogsForHabit(int habitId)
        {
            return await _habitRepository.GetLogsForHabit(habitId);
        }

        public Task<Habit> SaveHabit(Habit habit)
        {
            return _habitRepository.SaveHabit(habit);
        }

        public Task<Habit> UpdateHabit(Habit habit)
        {
            return _habitRepository.UpdateHabit(habit);
        }

        public async Task<HabitLog> SaveHabitLog(char status, int userId, int habitId, string date)
        {
            if (!status.Equals('C') && !status.Equals('F') && !status.Equals('P'))
                return null;
            // Habit log not null, then update otherwise create a new one
            var habit = await GetHabitById(habitId);
            checkHabitStatusUpdate(status, habit);

            if (habit == null)
                return null;

            var habitLog = await GetHabitLogByIdDate(habitId, date); // need to get by date passed in
            if (habitLog == null)
                return await _habitRepository.SaveHabitLog(status, userId, habitId);
            else
                return await _habitRepository.UpdateHabitLog(status, habitId, date); //save by created date
        }

        /// <summary>
        /// Check for different status updates and send notification to user if habit is visible to friends
        /// </summary>
        private void checkHabitStatusUpdate(char status, Habit habit)
        {
            if (habit.VisibleToFriends)
            {
                // we need to retrieve the habit friends

                // send notification if completed
                if (status.Equals('C'))
                {

                }

                // send notification in case of failure
                if (status.Equals('F'))
                {

                }
            }
        }
    }
}
