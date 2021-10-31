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

        public async Task<HabitStatDTO> GetGlobalHabitStats(Guid userId, int year)
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
                averageSuccessRate = avgSuccessRate,
                monthlySuccessRate = monthlySuccessRate,
                perfectDays = perfectDays,
                totalFriendsHelping = totalFriendsHelping,
                totalCompletions = totalCompletions
            };
        }

        public async Task<int> GetCurrentStreak(Guid userId)
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
                    if (habitLogs.Count > 1)
                    {
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
                    }
                    else
                    {
                        streak++;
                    }
                    

                    if (streak > currentStreak)
                        currentStreak = streak;
                });
            }

            return currentStreak;
        }

        public async Task<int> GetMaxStreak(Guid userId)
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
                    if (habitLogs.Count > 1)
                    {
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
                    }
                    else
                    {
                        streak++;
                    }
                    

                    // if the local variable streak is higher than maxStreak, then update maxStreak!
                    if (streak > maxStreak)
                        maxStreak = streak;
                });
            }

            return maxStreak;
        }

        public async Task<double> GetAverageSuccessRate(Guid userId)
        {
            return await this._habitStatRepository.GetGlobalSuccessRate(userId);
        }

        public async Task<Dictionary<string, double>> GetMonthlySuccessRate(Guid userId, int year)
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

        public async Task<int> GetPerfectDays(Guid userId)
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

        public async Task<int> GetTotalFriendsSupporting(Guid userId)
        {
            return await _habitStatRepository.GetFriendStat(userId);
        }

        public async Task<int> GetTotalCompletions(Guid userId)
        {
            var habitLogs = await _habitService.GetAllCompletedLogsForUser(userId);
            return habitLogs?.Count ?? 0;
        }
        #endregion

        #region Habit Stats
        public async Task<HabitStatDTO> GetHabitStats(Guid habitId, int year)
        {
            var currentStreak = await GetCurrentStreakForHabit(habitId);
            var maxStreak = await GetMaxStreakForHabit(habitId);
            var monthlySuccessRate = await GetMonthlySuccessRateForHabit(habitId, year);
            var perfectDays = await GetPerfectDaysForHabit(habitId);
            var totalFriendsHelping = await GetTotalFriendsHelpingForHabit(habitId);
            var totalCompletions = await GetTotalCompletionsForHabit(habitId);

            return new HabitStatDTO()
            {
                currentStreak = currentStreak,
                maxStreak = maxStreak,
                averageSuccessRate = -1.0,
                monthlySuccessRate = monthlySuccessRate,
                perfectDays = perfectDays,
                totalFriendsHelping = totalFriendsHelping,
                totalCompletions = totalCompletions
            };
        }
        public async Task<int> GetCurrentStreakForHabit(Guid habitId)
        {
            var currentStreak = 0;
            var logs = await _habitService.GetLogsForHabit(habitId);
            var streak = 0;
            if (logs != null && logs.Count != 0)
            {
                var completedLogs = logs.FindAll(o => o.Value == 'C');
                if (completedLogs.Count > 1)
                {
                    for (int i = completedLogs.Count - 1; i >= 0; i--)
                    {
                        if (i > 0)
                        {
                            // subtract latest day from previous
                            if ((completedLogs[i].CreatedAt.Date - completedLogs[i - 1].CreatedAt.Date).Days == 1)
                                streak++;
                        }
                        else
                        {
                            if (Math.Abs((completedLogs[i].CreatedAt.Date - completedLogs[i + 1].CreatedAt.Date).Days) == 1)
                                streak++;
                        }
                    }
                }
                else
                {
                    streak++;
                }

                if (streak > currentStreak)
                    currentStreak = streak;
            }

            return currentStreak;
        }

        public async Task<int> GetMaxStreakForHabit(Guid habitId)
        {
            // This will be the max streak, the value returned
            var maxStreak = 0;

            // Get all habit logs for a given user all habits ---- can call once
            var logs = await _habitService.GetLogsForHabit(habitId);
            var streak = 0;

            if (logs != null && logs.Count != 0)
            {
                var completedLogs = logs.FindAll(o => o.Value == 'C');
                if (completedLogs.Count > 1)
                {
                    for (int i = 0; i < completedLogs.Count; i++)
                    {
                        // if difference between next date and current date is 1 then add to local streak variable
                        if (i == completedLogs.Count - 1)
                        {
                            // check the previous streak
                            if ((completedLogs[i].CreatedAt.Date - completedLogs[i - 1].CreatedAt.Date).Days == 1)
                                streak++;
                        }
                        else
                        {
                            if (i == completedLogs.Count - 1 || (completedLogs[i + 1].CreatedAt.Date - completedLogs[i].CreatedAt.Date).Days == 1)
                                streak++;
                        }
                    }
                }
                else
                {
                    streak++;
                }

                // if the local variable streak is higher than maxStreak, then update maxStreak!
                if (streak > maxStreak)
                    maxStreak = streak;
            }

            return maxStreak;
        }

        public async Task<Dictionary<string, double>> GetMonthlySuccessRateForHabit(Guid habitId, int year)
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

        public async Task<int> GetPerfectDaysForHabit(Guid habitId)
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

        public async Task<int> GetTotalFriendsHelpingForHabit(Guid habitId)
        {
            return await _habitStatRepository.GetFriendStat(habitId);
        }

        public async Task<int> GetTotalCompletionsForHabit(Guid habitId)
        {
            var habitLogs = await _habitService.GetAllCompletedLogsForHabit(habitId);
            return habitLogs?.Count ?? 0;
        }
        #endregion
    }
}
