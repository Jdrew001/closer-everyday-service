using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Services.Interfaces
{
    public interface IHabitStatService
    {
        #region Global Stats for a given user

        public Task<int> GetCurrentStreak(int userId);
        public Task<int> GetMaxStreak(int userId);
        public Task<int> GetAverageSuccessRate(int userId);
        public Task<int[]> GetMonthlySuccessRate(int userId);
        public Task<int> GetPerfectDays(int userId);
        public Task<int> GetTotalFriendsSupporting(int userId);
        public Task<int> GetTotalCompletions(int userId);

        #endregion

        #region Habit Specific Stats

        public Task<int> GetCurrentStreakForHabit(int habitId);
        public Task<int> GetMaxStreakForHabit(int habitId);
        public Task<int[]> GetMonthlySuccessRateForHabit(int habitId);
        public Task<int> GetPerfectDaysForHabit(int habitId);
        public Task<int> GetTotalFriendsHelpingForHabit(int habitId);
        public Task<int> GetTotalCompletionsForHabit(int habitId);

        #endregion
    }
}
