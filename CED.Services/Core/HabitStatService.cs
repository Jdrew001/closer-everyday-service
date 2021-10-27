using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CED.Data.Interfaces;
using CED.Models.DTO;
using CED.Services.Interfaces;
using CED.Services.utils;
using Org.BouncyCastle.Crypto.Engines;

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

        public async Task<HabitStatDTO> GetGlobalHabitStats(int userId, int year)
        {
            var currentStreak = await GetCurrentStreak(userId);
            var maxStreak = await GetMaxStreak(userId);
            var avgSuccessRate = await GetAverageSuccessRate(userId);
            var monthlySuccessRate = await GetMonthlySuccessRate(userId, year);
            var perfectDays = await GetPerfectDays(userId);
            var totalFriendsHelping = await GetTotalFriendsSupporting(userId);
            var totalCompletions = await GetTotalCompletions(userId);

            return new HabitStatDTO()
            {
                currentStreak = currentStreak,
                maxStreak = maxStreak,
                averageSuccessReate = avgSuccessRate,
                monthlySuccessRate = monthlySuccessRate,
                perfectDays = perfectDays,
                totalFriendsHelping = totalFriendsHelping,
                totalCompletions = totalCompletions
            };
        }

        public async Task<int> GetCurrentStreak(int userId)
        {
            var currentStreak = 0;

            // I want to get all completed user habits logs
            var logs = await _habitService.GetAllCompletedLogsForUser(userId);

            if (logs != null)
            {
                var habitIds = logs.Select(o => o.HabitId).Distinct().ToList();

                habitIds.ForEach(habitId => {
                    var habitLogs = logs.FindAll(o => o.HabitId == habitId);
                    var streak = 0;

                    for (int i = habitLogs.Count - 1; i >= 0; i--)
                    {
                        if (i > 0)
                        {
                            // subtract latest day from previous
                            if ((habitLogs[i].CreatedAt.Date - habitLogs[i - 1].CreatedAt.Date).Days == 1)
                                streak++;
                        }
                        else
                        {
                            if (Math.Abs((habitLogs[i].CreatedAt.Date - habitLogs[i + 1].CreatedAt.Date).Days) == 1)
                                streak++;
                        }
                    }

                    if (streak > currentStreak)
                        currentStreak = streak;
                });
            }

            return currentStreak;
        }

        public async Task<int> GetMaxStreak(int userId)
        {
            // This will be the max streak, the value returnedd
            var maxStreak = 0;

            // Get all habit logs for a given user all habits ---- can call once
            var logs = await _habitService.GetAllCompletedLogsForUser(userId);

            if (logs != null)
            {
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
                            if ((habitLogs[i].CreatedAt.Date - habitLogs[i - 1].CreatedAt.Date).Days == 1)
                                streak++;
                        }
                        else
                        {
                            if (i == habitLogs.Count - 1 || (habitLogs[i + 1].CreatedAt.Date - habitLogs[i].CreatedAt.Date).Days == 1)
                                streak++;
                        }
                    }

                    // if the local variable streak is higher than maxStreak, then update maxStreak!
                    if (streak > maxStreak)
                        maxStreak = streak;
                });
            }

            return maxStreak;
        }

        public async Task<double> GetAverageSuccessRate(int userId)
        {
            return await this._habitStatRepository.GetGlobalSuccessRate(userId);
        }

        public async Task<Dictionary<string, double>> GetMonthlySuccessRate(int userId, int year)
        {
            var habitLogs = await _habitService.GetUserHabitLogs(userId);
            Dictionary<string, double> monthlyRates = new Dictionary<string, double>();

            if (habitLogs != null)
            {
                var habitLogsForGivenYear = habitLogs.FindAll(o => o.CreatedAt.Year == year);

                //loop through the months
                foreach (var month in ServiceConstants.MONTHS_OF_YEAR)
                {
                    var habitsForMonth = habitLogsForGivenYear.FindAll(o => o.CreatedAt.Month == month.Key);

                    // rate: (total complete / total) * 100 percentage
                    double totalComplete = Convert.ToDouble(habitsForMonth.FindAll(o => o.Value == 'C').Count);
                    double total = Convert.ToDouble(habitsForMonth.Count);
                    double rate = 0;

                    if (totalComplete != 0 && total != 0)
                        rate = (totalComplete / total) * 100;

                    monthlyRates.Add(month.Value, rate);
                }
            }

            return monthlyRates;
        }

        public async Task<int> GetPerfectDays(int userId)
        {
            var perfectDays = 0;
            //Get all users habit logs
            var logs = await _habitService.GetUserHabitLogs(userId);

            if (logs != null)
            {
                var habitLogDates = logs.Select(o => o.CreatedAt).Distinct().ToList();

                habitLogDates.ForEach(date =>
                {
                    var count = logs.FindAll(o => o.CreatedAt.Date == date.Date);
                    var completedLogs = logs.FindAll(o => o.CreatedAt.Date == date.Date && o.Value == 'C');

                    if (count.Count == completedLogs.Count)
                        perfectDays++;
                });
            }

            return perfectDays;
        }

        public async Task<int> GetTotalFriendsSupporting(int userId)
        {
            return await _habitStatRepository.GetFriendStat(userId);
        }

        public async Task<int> GetTotalCompletions(int userId)
        {
            var habitLogs = await _habitService.GetAllCompletedLogsForUser(userId);
            return habitLogs?.Count ?? 0;
        }
        #endregion

        #region Habit Stats
        public async Task<int> GetCurrentStreakForHabit(int habitId)
        {
            var currentStreak = 0;
            var logs = await _habitService.GetLogsForHabit(habitId);
            var streak = 0;
            if (logs != null)
            {
                for (int i = logs.Count - 1; i >= 0; i--)
                {
                    if (i > 0)
                    {
                        // subtract latest day from previous
                        if ((logs[i].CreatedAt.Date - logs[i - 1].CreatedAt.Date).Days == 1)
                            streak++;
                    }
                    else
                    {
                        if (Math.Abs((logs[i].CreatedAt.Date - logs[i + 1].CreatedAt.Date).Days) == 1)
                            streak++;
                    }
                }

                if (streak > currentStreak)
                    currentStreak = streak;
            }

            return currentStreak;
        }

        public async Task<int> GetMaxStreakForHabit(int habitId)
        {
            // This will be the max streak, the value returnedd
            var maxStreak = 0;

            // Get all habit logs for a given user all habits ---- can call once
            var logs = await _habitService.GetLogsForHabit(habitId);
            var streak = 0;

            if (logs != null)
            {
                for (int i = 0; i < logs.Count; i++)
                {
                    // if difference between next date and current date is 1 then add to local streak variable
                    if (i == logs.Count - 1)
                    {
                        // check the previous streak
                        if ((logs[i].CreatedAt.Date - logs[i - 1].CreatedAt.Date).Days == 1)
                            streak++;
                    }
                    else
                    {
                        if (i == logs.Count - 1 || (logs[i + 1].CreatedAt.Date - logs[i].CreatedAt.Date).Days == 1)
                            streak++;
                    }
                }

                // if the local variable streak is higher than maxStreak, then update maxStreak!
                if (streak > maxStreak)
                    maxStreak = streak;
            }

            return maxStreak;
        }

        public async Task<Dictionary<string, double>> GetMonthlySuccessRateForHabit(int habitId, int year)
        {
            var habitLogs = await _habitService.GetLogsForHabit(habitId);
            Dictionary<string, double> monthlyRates = new Dictionary<string, double>();

            if (habitLogs != null)
            {
                var habitLogsForGivenYear = habitLogs.FindAll(o => o.CreatedAt.Year == year);

                //loop through the months
                foreach (var month in ServiceConstants.MONTHS_OF_YEAR)
                {
                    var habitsForMonth = habitLogsForGivenYear.FindAll(o => o.CreatedAt.Month == month.Key);

                    // rate: (total complete / total) * 100 percentage
                    double totalComplete = Convert.ToDouble(habitsForMonth.FindAll(o => o.Value == 'C').Count);
                    double total = Convert.ToDouble(habitsForMonth.Count);
                    double rate = 0;

                    if (totalComplete != 0 && total != 0)
                        rate = (totalComplete / total) * 100;

                    monthlyRates.Add(month.Value, rate);
                }
            }

            return monthlyRates;
        }

        public async Task<int> GetPerfectDaysForHabit(int habitId)
        {
            var perfectDays = 0;
            //Get all users habit logs
            var logs = await _habitService.GetLogsForHabit(habitId);

            if (logs != null)
            {
                var habitLogDates = logs.Select(o => o.CreatedAt).Distinct().ToList();
                habitLogDates.ForEach(date =>
                {
                    var count = logs.FindAll(o => o.CreatedAt.Date == date.Date);
                    var completedLogs = logs.FindAll(o => o.CreatedAt.Date == date.Date && o.Value == 'C');

                    if (count.Count == completedLogs.Count)
                        perfectDays++;
                });
            }

            return perfectDays;
        }

        public async Task<int> GetTotalFriendsHelpingForHabit(int habitId)
        {
            return await _habitStatRepository.GetFriendStat(habitId);
        }

        public async Task<int> GetTotalCompletionsForHabit(int habitId)
        {
            var habitLogs = await _habitService.GetAllCompletedLogsForHabit(habitId);
            return habitLogs?.Count ?? 0;
        }
        #endregion
    }
}
