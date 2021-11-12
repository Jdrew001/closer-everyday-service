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

namespace CED.Services.Core
{
    public class MilestoneService : IMilestoneService
    {
        private readonly IHabitService _habitService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IMilestoneRepository _milestoneRepository;

        public MilestoneService(
            IHabitService habitService,
            INotificationService notificationService,
            IUserService userService,
            IMilestoneRepository milestoneRepository)
        {
            _habitService = habitService;
            _notificationService = notificationService;
            _userService = userService;
            _milestoneRepository = milestoneRepository;
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
            var type = await GetMilestoneType(MileStoneScope.Global, subType);
            return await _milestoneRepository.CreateGlobalMilestone(type.Id, userId, value);
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
        public async void CheckForGlobalCompletions(Guid userId, List<HabitLog> logs)
        {
            var completedLogsLength = logs.FindAll(o => o.Value.ToString().ToLower() == "c").Count;
            var lengths = MilestoneConstants.VALUES;
            foreach (int entry in lengths)
            {
                if (completedLogsLength == entry)
                {
                    // TODO: create new milestone
                    var milestone = await GetMilestoneByType(MileStoneScope.Global, MileStoneSubType.Perfect, entry.ToString());
                    if (milestone == null)
                    {
                        await CreateGlobalMilestone(MileStoneSubType.Completion, userId, entry.ToString());
                    }
                    break;
                }
            }
        }

        public async void GlobalPerfectDays(Guid userId, List<HabitLog> logs)
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

            var milestone = await GetMilestoneByType(MileStoneScope.Global, MileStoneSubType.Perfect, perfectDays.ToString());
            if (milestone == null)
            {
                await CreateGlobalMilestone(MileStoneSubType.Perfect, userId, perfectDays.ToString());
            }
        }
        #endregion
    }
}
