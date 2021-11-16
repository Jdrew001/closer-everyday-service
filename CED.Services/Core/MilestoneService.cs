using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CED.Data.Interfaces;
using CED.Models.Core;
using CED.Services.Interfaces;
using CED.Services.utils;
using Microsoft.Extensions.Logging;

namespace CED.Services.Core
{
    public class MilestoneService : IMilestoneService
    {
        private readonly IHabitService _habitService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IMilestoneRepository _milestoneRepository;
        private readonly ILogger<MilestoneService> _log;

        public MilestoneService(
            IHabitService habitService,
            INotificationService notificationService,
            IUserService userService,
            IMilestoneRepository milestoneRepository,
            ILogger<MilestoneService> log)
        {
            _habitService = habitService;
            _notificationService = notificationService;
            _userService = userService;
            _milestoneRepository = milestoneRepository;
            _log = log;
        }

        public HabitLog PreviousLog { get; set; }

        public Task<Milestone> GetMilestoneByType(MileStoneScope type, MileStoneSubType subType, string value)
        {
            return _milestoneRepository.GetMilestoneByType(type, subType, value);
        }

        public Task<MilestoneType> GetMilestoneType(MileStoneScope type, MileStoneSubType subType)
        {
            return _milestoneRepository.GetMilestoneType(type, subType);
        }

        public async Task<Milestone> CreateGlobalMilestone(MileStoneSubType subType, Guid userId, string value)
        {
            Milestone milestone = null;
            try
            {
                MilestoneType type = await GetMilestoneType(MileStoneScope.Global, subType);
                if (type != null) 
                    milestone = await _milestoneRepository.CreateGlobalMilestone(type.Id, userId, value);
            }
            catch (Exception e)
            {
                _log.LogCritical(e, 
                    "Exception inside (CreateGlobalMilestone) Sub Type {subType}, User {userId}, Milestone Value {value}", subType, userId, value);
                throw;
            }
            return milestone;
        }

        public async Task<Milestone> CreateHabitMilestone(MileStoneSubType subType, Guid userId, Guid habitId, string value)
        {
            var type = await GetMilestoneType(MileStoneScope.Habit, subType);
            return await _milestoneRepository.CreateHabitMilestone(type.Id, userId, habitId, value);
        }

        public async void CheckForGlobalMilestones(Guid userId)
        {
            var logs = await _habitService.GetUserHabitLogs(userId);
            CheckForGlobalCompletions(userId, logs);
        }

        public void CheckForHabitMilestones(Guid userId, Guid habitId)
        {
            throw new NotImplementedException();
        }

        #region Global Milestones
        public void CheckForGlobalCompletions(Guid userId, List<HabitLog> logs)
        {
            var completedLogsLength = logs.FindAll(o => o.Value.ToString().ToLower() == "c").Count;
            var lengths = MilestoneConstants.VALUES;
            CheckMilestoneLength(completedLogsLength, MileStoneScope.Global, MileStoneSubType.Completion, userId);
        }

        public void CheckForGlobalPerfectDays(Guid userId, List<HabitLog> logs)
        {
            int perfectDays = 0;
            // Get all distinct dates for a user's logs
            var dates = logs.Select(o => o.CreatedAt).Distinct().ToList();
            
            foreach (var date in dates)
            {
                var logsForDate = logs.FindAll(o => o.CreatedAt == date).Distinct().ToList();
                var completedLogCountForDate = logsForDate.FindAll(o => o.Value.ToString().ToLower() == "c");

                if (completedLogCountForDate.Count == logsForDate.Count)
                {
                    perfectDays++;
                }
            }
            CheckMilestoneLength(perfectDays, MileStoneScope.Global, MileStoneSubType.Perfect, userId);       
        }

        public void CheckForGlobalHabitStreak(Guid userId, List<HabitLog> logs)
        {
            int streak = 0;
            var dates = logs.Select(o => o.CreatedAt).Distinct().ToList();

            foreach (var date in dates)
            {
                var logsForDate = logs.FindAll(o => o.CreatedAt == date).Distinct().ToList();
                var completedLogCountForDate = logsForDate.FindAll(o => o.Value.ToString().ToLower() == "c");

                if (completedLogCountForDate.Count >= 1)
                {
                    streak++;
                } 
                else
                {
                    streak = 0;
                }
            }
            CheckMilestoneLength(streak, MileStoneScope.Global, MileStoneSubType.Streak, userId);
        }

        public void CheckForGlobalFriendsSupported(Guid userId, List<Habit> habits)
        {
            try
            {
                if (habits != null)
                {
                    List<FriendHabit> friends = new();
                    habits.ForEach(habit =>
                    {
                        List<FriendHabit> friendhabits = habit.friendHabits;
                        habit.friendHabits.ForEach(item =>
                        {
                            var friend = friends.Find(o => o.FriendId == item.FriendId);
                            if (friend == null)
                                friends.Add(item);
                        });
                    });
                    CheckMilestoneLength(friends.Count(), MileStoneScope.Global, MileStoneSubType.FriendsSupported, userId);
                }
            } 
            catch(Exception e)
            {
                _log.LogCritical(e, "Exception inside (CheckForGlobalFriendsSupported) User {userId}, Habits {habits}", userId, habits);
            }
            
        }
        #endregion

        private async void CheckMilestoneLength(int value, MileStoneScope scope, MileStoneSubType type, Guid userId)
        {
            var lengths = MilestoneConstants.VALUES;
            foreach (int entry in lengths)
            {
                if (value == entry)
                {
                    var milestone = await GetMilestoneByType(scope, type, value.ToString());
                    if (milestone == null)
                    {
                        await CreateGlobalMilestone(type, userId, value.ToString());
                    }
                    break;
                }
            }
        }
    }
}
