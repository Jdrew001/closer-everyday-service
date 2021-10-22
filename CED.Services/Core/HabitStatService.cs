using System;
using System.Linq;
using System.Threading.Tasks;
using CED.Data.Interfaces;
using CED.Services.Interfaces;

namespace CED.Services.Core
{
    public class HabitStatService : IHabitStatService
    {
        private readonly IHabitService _habitService;
        private readonly IHabitStatRepository _habitStatRepository;
        public HabitStatService(IHabitService habitService, IHabitStatRepository habitStatRepository)
        {
            _habitService = habitService;
            _habitStatRepository = habitStatRepository;
        }

        #region global stats for user
        public async Task<int> GetCurrentStreak(int userId)
        {
            var currentStreak = 0;

            // I want to get all completed user habits logs
            var logs = await _habitService.GetAllCompletedLogsForUser(userId);
            var habitIds = logs.Select(o => o.HabitId).Distinct().ToList();

            habitIds.ForEach(habitId => {
                var habitLogs = logs.FindAll(o => o.HabitId == habitId);
                var streak = 0;

                for (int i = habitLogs.Count - 1; i >= 0; i--)
                {
                    if (i > 0)
                    {
                        // subtract latest day from previous
                        if ((habitLogs[i].CreatedAt - habitLogs[i - 1].CreatedAt).Days == 1)
                            streak++;
                    }
                    else
                    {
                        if (Math.Abs((habitLogs[i].CreatedAt - habitLogs[i + 1].CreatedAt).Days) == 1)
                            streak++;
                    }
                }

                if (streak > currentStreak)
                    currentStreak = streak;
            });

            return currentStreak;
        }

        public async Task<int> GetMaxStreak(int userId)
        {
            // This will be the max streak, the value returnedd
            var maxStreak = 0;

            // Get all habit logs for a given user all habits ---- can call once
            var logs = await _habitService.GetAllCompletedLogsForUser(userId);

            // Grab all the habit ids and remove duplicates
            var habitIds = logs.Select(o => o.HabitId).Distinct().ToList();

            // loop through all ids
            habitIds.ForEach(habitId =>
            {
                // find all logs for a given habit id
                var habitLogs = logs.FindAll(o => o.HabitId == habitId);
                var streak = 0;

                for (int i = 0; i < habitLogs.Count; i++)
                {
                    // if difference between next date and current date is 1 then add to local streak variable
                    if (i == habitLogs.Count - 1)
                    {
                        // check the previous streak
                        if ((habitLogs[i].CreatedAt - habitLogs[i - 1].CreatedAt).Days == 1)
                            streak++;
                    }
                    else
                    {
                        if (i == habitLogs.Count - 1 || (habitLogs[i + 1].CreatedAt - habitLogs[i].CreatedAt).Days == 1)
                            streak++;
                    }
                }

                // if the local variable streak is higher than maxStreak, then update maxStreak
                if (streak > maxStreak)
                    maxStreak = streak;
            });

            return maxStreak;
        }

        public async Task<double> GetAverageSuccessRate(int userId)
        {
            return await this._habitStatRepository.GetGlobalSuccessRate(userId);
        }

        public async Task<int[]> GetMonthlySuccessRate(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetPerfectDays(int userId)
        {
            //Get all users habit logs
            var logs = await _habitService.GetUserHabitLogs(userId);
            var habitLogDates = logs.Select(o => o.CreatedAt).Distinct().ToList();
            var perfectDays = 0;

            habitLogDates.ForEach(date =>
            {
                var count = logs.FindAll(o => o.CreatedAt.Date == date.Date);
                var completedLogs = logs.FindAll(o => o.CreatedAt.Date == date.Date && o.Value == 'C');

                if (count.Count == completedLogs.Count)
                    perfectDays++;
            });

            return perfectDays;
        }

        public async Task<int> GetTotalFriendsSupporting(int userId)
        {
            return await _habitStatRepository.GetFriendStat(userId);
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
