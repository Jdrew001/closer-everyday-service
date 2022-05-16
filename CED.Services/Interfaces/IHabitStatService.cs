using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Models.DTO;

namespace CED.Services.Interfaces
{
    public interface IHabitStatService
    {
        #region Global Stats for a given user

        public Task<HabitStatDTO> GetGlobalHabitStats(Guid userId, int year);
        public Task<HabitStatDTO> GetHabitStats(Guid habitId, int year);
        public Task<int> GetCurrentStreak(Guid userId);
        public Task<int> GetMaxStreak(Guid userId);
        public Task<double> GetAverageSuccessRate(Guid userId);
        public Task<Dictionary<string, double>> GetMonthlySuccessRate(Guid userId, int year);
        public Task<int> GetPerfectDays(Guid userId);
        public Task<int> GetTotalFriendsSupporting(Guid userId);
        public Task<int> GetTotalCompletions(Guid userId);

        #endregion

        #region Habit Specific Stats

        public Task<int> GetCurrentStreakForHabit(Guid habitId);
        public Task<int> GetMaxStreakForHabit(Guid habitId);
        public Task<Dictionary<string, double>> GetMonthlySuccessRateForHabit(Guid habitId, int year);
        public Task<int> GetPerfectDaysForHabit(Guid habitId);
        public Task<int> GetTotalFriendsHelpingForHabit(Guid habitId);
        public Task<int> GetTotalCompletionsForHabit(Guid habitId);

        #endregion
    }
}
