﻿using System;
using System.Linq;
using System.Threading.Tasks;
using CED.Services.Interfaces;

namespace CED.Services.Core
{
    public class HabitStatService : IHabitStatService
    {
        private readonly IHabitService _habitService;
        public HabitStatService(IHabitService habitService)
        {
            _habitService = habitService;
        }

        #region global stats for user
        public async Task<int> GetCurrentStreak(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetMaxStreak(int userId)
        {
            // This will be the max streak, the value returnedd
            var maxStreak = 0;

            // Get all habit logs for a given user all habits
            var logs = await _habitService.GetAllHabitLogsForUser(userId);

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
