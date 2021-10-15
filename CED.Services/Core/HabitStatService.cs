using System;
using System.Threading.Tasks;
using CED.Services.Interfaces;

namespace CED.Services.Core
{
    public class HabitStatService : IHabitStatService
    {
        #region global stats for user
        public async Task<int> GetCurrentStreak(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetMaxStreak(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetAverageSuccessRate(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int[]> GetMonthlySuccessRate(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetPerfectDays(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalFriendsSupporting(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalCompletions(int userId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Habit Stats
        public async Task<int> GetCurrentStreakForHabit(int habitId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetMaxStreakForHabit(int habitId)
        {
            throw new NotImplementedException();
        }

        public async Task<int[]> GetMonthlySuccessRateForHabit(int habitId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetPerfectDaysForHabit(int habitId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalFriendsHelpingForHabit(int habitId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalCompletionsForHabit(int habitId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
