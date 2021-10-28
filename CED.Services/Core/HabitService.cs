using System;
using CED.Data.Interfaces;
using CED.Models.Core;
using CED.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CED.Services.Core
{
    public class HabitService : IHabitService
    {
        private readonly ILogger<HabitService> _log;
        private readonly IHabitRepository _habitRepository;
        private readonly IFrequencyService _frequencyService;
        private readonly IScheduleService _scheduleService;
        private readonly IFriendService _friendService;
        private readonly IUserRepository _userRepository;

        public HabitService(
            ILogger<HabitService> log,
            IHabitRepository habitRepository,
            IFrequencyService frequencyService,
            IScheduleService scheduleService,
            IFriendService friendService,
            IUserRepository userRepository)
        {
            _habitRepository = habitRepository;
            _frequencyService = frequencyService;
            _friendService = friendService;
            _scheduleService = scheduleService;
            _userRepository = userRepository;
            _log = log;
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

        public async Task<List<HabitLog>> GetAllCompletedLogsForHabit(int habitId)
        {
            return await _habitRepository.GetAllCompletedLogsForHabit(habitId);
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

        public async Task<Habit> SaveHabit(Habit habit)
        {
            Habit savedHabit = null;
            _log.LogDebug("BEGIN SaveHabit - {Habit}", habit);
            try
            {
                _log.LogDebug("Saving schedule : {Schedule}", habit.Schedule);
                Schedule schedule = await _scheduleService.SaveSchedule(habit.Schedule);
                _log.LogDebug("Saved schedule : {Schedule}", habit.Schedule);

                
                habit.Schedule = schedule;

                //Save Habit
                _log.LogDebug("Saving habit : {Habit}", habit);
                savedHabit = await _habitRepository.SaveHabit(habit);
                _log.LogDebug("Saved habit : {Habit}", habit);

                // Save frequencies
                _log.LogDebug("Saving Frequencies : {Frequencies}", habit.Frequencies);
                var frequencies = await _frequencyService.SaveHabitFrequencies(habit.Frequencies, savedHabit.Id);
                habit.Frequencies = frequencies;
                _log.LogDebug("Saved Frequencies : {Frequencies}", habit.Frequencies);

                var habitFriends = new List<FriendHabit>();
                _log.LogDebug("Saving Habit Friends : {Habit Friends}", habit.friendHabits);
                // save friend habits
                habit.friendHabits.ForEach(o =>
                {
                    var habitFriend = _friendService.SaveFriendToHabit(o.FriendId, habit.Id, habit.UserId).Result;
                    if (habitFriend != null)
                        habitFriends.Add(habitFriend);
                });

                habit.friendHabits = habitFriends;
                _log.LogDebug("Saved Habit Friends : {Habit Friends}", habit.friendHabits);
            }
            catch (Exception e)
            {
                _log.LogCritical(e, "ERROR: Exception occurred in (SaveHabit) Habit : {habit}, User : {user}", habit, habit.UserId);
                return null;
            }

            _log.LogDebug("END SaveHabit - {Habit}", savedHabit);
            return savedHabit;
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
