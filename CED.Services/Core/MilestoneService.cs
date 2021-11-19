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

        /// <summary>
        /// Create a global milestone for all habits
        /// </summary>
        /// <param name="subType">Milestone SubType Ex: Completion, Perfect</param>
        /// <param name="userId">Guid User id</param>
        /// <param name="value">Value to save</param>
        /// <returns>Task Milestone</returns>
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

        /// <summary>
        /// Create a new milestone for a habit once a condition is satisfied
        /// </summary>
        /// <param name="subType">Milestone Subtype Ex: Completion, Perfect</param>
        /// <param name="userId">Guid user id</param>
        /// <param name="habitId">Guid habit id</param>
        /// <param name="value">The milestone value</param>
        /// <returns>Task Milestone</returns>
        public async Task<Milestone> CreateHabitMilestone(MileStoneSubType subType, Guid userId, Guid habitId, string value)
        {
            var type = await GetMilestoneType(MileStoneScope.Habit, subType);
            return await _milestoneRepository.CreateHabitMilestone(type.Id, userId, habitId, value);
        }

        /// <summary>
        /// Check for Global milestones for a user
        /// </summary>
        /// <param name="userId">Guid User Id</param>
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

        /// <summary>
        /// A perfect day constitutes all logs for a given date are complete
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="logs"></param>
        public void CheckForGlobalPerfectDays(Guid userId, List<HabitLog> logs)
        {
            var perfectDays = ComputePerfectDays(logs);
            CheckMilestoneLength(perfectDays, MileStoneScope.Global, MileStoneSubType.Perfect, userId);       
        }

        /// <summary>
        /// For all habits, atleast one completed habit will increase the streak for the user
        /// </summary>
        /// <param name="userId">Guid id for user</param>
        /// <param name="logs">Habit logs for a given user habits</param>
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

        /// <summary>
        /// For a given user, check all the friends that is being supported
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="habits"></param>
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

        #region Habit Milestones
        /// <summary>
        /// For a given habit, check for completions
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="habitId"></param>
        /// <param name="logs"></param>
        public void CheckForHabitCompletions(Guid userId, Guid habitId, List<HabitLog> logs)
        {
            var completedLogsLength = logs.FindAll(o => o.Value.ToString().ToLower() == "c").Count;
            var lengths = MilestoneConstants.VALUES;
            CheckMilestoneLength(completedLogsLength, MileStoneScope.Habit, MileStoneSubType.Completion, userId, habitId);
        }

        /// <summary>
        /// For a given habit, check for dates that have perfect days
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="habitId"></param>
        /// <param name="logs"></param>
        public void CheckForHabitPerfect(Guid userId, Guid habitId, List<HabitLog> logs)
        {
            var perfectDays = ComputePerfectDays(logs);
            CheckMilestoneLength(perfectDays, MileStoneScope.Habit, MileStoneSubType.Perfect, userId);
        }
        #endregion

        private async void CheckMilestoneLength(int value, MileStoneScope scope, MileStoneSubType type, Guid userId, Guid habitId = new Guid())
        {
            var lengths = type == MileStoneSubType.FriendsSupported ? MilestoneConstants.FRIEND_VALUE: MilestoneConstants.VALUES;
            foreach (int entry in lengths)
            {
                if (value == entry)
                {
                    var milestone = await GetMilestoneByType(scope, type, value.ToString());
                    if (milestone == null)
                    {
                        if (scope == MileStoneScope.Global)
                        {
                            await CreateGlobalMilestone(type, userId, value.ToString());
                        }
                        else
                        {
                            await CreateHabitMilestone(type, userId, habitId, value.ToString());
                        }
                    }
                    break;
                }
            }
        }
    
        private int ComputePerfectDays(List<HabitLog> logs)
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

            return perfectDays;
        }
    }
}
