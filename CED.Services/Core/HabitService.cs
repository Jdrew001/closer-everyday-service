using System;
using CED.Data.Interfaces;
using CED.Models.Core;
using CED.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using CED.Models.DTO;
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

        public HabitService(
            ILogger<HabitService> log,
            IHabitRepository habitRepository,
            IFrequencyService frequencyService,
            IScheduleService scheduleService,
            IFriendService friendService)
        {
            _habitRepository = habitRepository;
            _frequencyService = frequencyService;
            _friendService = friendService;
            _scheduleService = scheduleService;
            _log = log;
        }

        public Task<List<Habit>> GetAllHabits()
        {
            return _habitRepository.GetAllHabits();
        }

        public async Task<List<Habit>> GetAllUserHabits(Guid userId, string date)
        {
            var habits = await _habitRepository.GetAllUserHabits(userId, date);
            habits.ForEach(async o =>
            {
                o.Frequencies = await _frequencyService.GetHabitFrequencies(o.Id);
                o.friendHabits = await _friendService.GetFriendsForHabit(o.Id);
                o.habitLog = await GetHabitLogByIdDate(o.Id, date);
            });
            return habits;
        }

        public Task<Habit> GetHabitById(Guid id)
        {
            return _habitRepository.GetHabitById(id);
        }

        public Task<HabitLog> GetHabitLogByIdDate(Guid id, string date)
        {
            return _habitRepository.GetHabitLogByIdAndDate(id, date);
        }

        public Task<HabitLog> GetHabitLog(Guid id)
        {
            return _habitRepository.GetHabitLogById(id);
        }

        public Task<List<HabitLog>> GetAllCompletedLogsForUser(Guid userId)
        {
            return _habitRepository.GetAllCompletedLogsForUser(userId);
        }

        public async Task<List<HabitLog>> GetAllCompletedLogsForHabit(Guid habitId)
        {
            return await _habitRepository.GetAllCompletedLogsForHabit(habitId);
        }

        public async Task<List<HabitLog>> GetUserHabitLogs(Guid userId)
        {
            return await _habitRepository.GetAllLogsForUser(userId);
        }

        public bool MarkHabitInactive(Guid id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<HabitLog>> GetLogsForHabit(Guid habitId)
        {
            return await _habitRepository.GetLogsForHabit(habitId);
        }

        public async Task<Habit> SaveHabit(Habit habit)
        {
            Habit savedHabit = null;
            _log.LogInformation("BEGIN SaveHabit - {Habit}", habit);
            try
            {
                _log.LogInformation("Saving schedule : {Schedule}", habit.Schedule);
                Schedule schedule = await _scheduleService.SaveSchedule(habit.Schedule);
                _log.LogInformation("Saved schedule : {Schedule}", habit.Schedule);

                
                habit.Schedule = schedule;

                //Save Habit
                _log.LogInformation("Saving habit : {Habit}", habit);
                savedHabit = await _habitRepository.SaveHabit(habit);
                _log.LogInformation("Saved habit : {Habit}", habit);

                // Save frequencies
                _log.LogInformation("Saving Frequencies : {Frequencies}", habit.Frequencies);
                var frequencies = await _frequencyService.SaveHabitFrequencies(habit.Frequencies, savedHabit.Id);
                savedHabit.Frequencies = frequencies;
                _log.LogInformation("Saved Frequencies : {Frequencies}", habit.Frequencies);

                var habitFriends = new List<FriendHabit>();
                _log.LogInformation("Saving Habit Friends : {Habit Friends}", habit.friendHabits);
                // save friend habits
                habit.friendHabits.ForEach(async o =>
                {
                    var habitFriend = await _friendService.SaveFriendToHabit(o.FriendId, habit.Id, habit.UserId);
                    if (habitFriend != null)
                        habitFriends.Add(habitFriend);
                });

                savedHabit.friendHabits = habitFriends;
                _log.LogInformation("Saved Habit Friends : {Habit Friends}", habit.friendHabits);
            }
            catch (Exception e)
            {
                _log.LogCritical(e, "ERROR: Exception occurred in (SaveHabit) Habit : {habit}, User : {user}", habit, habit.UserId);
                return null;
            }

            _log.LogInformation("END SaveHabit - {Habit}", savedHabit);
            return savedHabit;
        }

        public async Task<Habit> UpdateHabit(Habit habit)
        {
            Habit updatedHabit = null;
            _log.LogInformation("BEGIN SaveHabit - {Habit}", habit);
            try
            {
                // Update schedule
                _log.LogInformation("Updating schedule : {Schedule}", habit.Schedule);
                Schedule schedule = await _scheduleService.UpdateSchedule(habit.Schedule);
                _log.LogInformation("Updated schedule : {Schedule}", habit.Schedule);

                habit.Schedule = schedule;

                //Update Habit
                _log.LogInformation("Updating habit : {Habit}", habit);
                updatedHabit = await _habitRepository.UpdateHabit(habit);
                _log.LogInformation("Updated habit : {Habit}", habit);

                // Update frequencies
                _log.LogInformation("Saving Frequencies : {Frequencies}", habit.Frequencies);
                var cleared = await _frequencyService.ClearHabitFrequencies(habit.Id);
                if (cleared.Count > 0)
                    return null;

                var frequencies = await _frequencyService.SaveHabitFrequencies(habit.Frequencies, updatedHabit.Id);
                updatedHabit.Frequencies = frequencies;
                _log.LogInformation("Saved Frequencies : {Frequencies}", habit.Frequencies);


                // TODO: Need to complete below
                var habitFriends = new List<FriendHabit>();
                _log.LogInformation("Updating Habit Friends : {Habit Friends}", habit.friendHabits);
                // Update friend habits
                habit.friendHabits.ForEach(async o =>
                {
                    await _friendService.ClearFriendToHabit(o.FriendId, habit.Id, habit.UserId);
                });

                habit.friendHabits.ForEach(async o =>
                {
                    var habitFriend = await _friendService.SaveFriendToHabit(o.FriendId, habit.Id, habit.UserId);
                    if (habitFriend != null)
                        habitFriends.Add(habitFriend);
                });

                updatedHabit.friendHabits = habitFriends;
                _log.LogInformation("Updated Habit Friends : {Habit Friends}", habit.friendHabits);
            }
            catch (Exception e)
            {
                _log.LogCritical(e, "ERROR: Exception occurred in (UpdateHabit) Habit : {habit}, User : {user}", habit, habit.UserId);
                return null;
            }

            _log.LogInformation("END Update Habit - {Habit}", updatedHabit);
            return updatedHabit;
        }

        public async Task<HabitLog> SaveHabitLog(char status, Guid userId, Guid habitId, string date)
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
