using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Models.Core;
using CED.Services.Core;

namespace CED.Services.Interfaces
{
    public interface IMilestoneService
    {
        public HabitLog PreviousLog { get; set; }
        public Task<MilestoneType> GetMilestoneType(MileStoneScope type, MileStoneSubType subType);
        public Task<Milestone> GetMilestoneByType(MileStoneScope type, MileStoneSubType subType, string value);
        public Task<Milestone> CreateGlobalMilestone(MileStoneSubType subType, Guid userId, string value);
        public Task<Milestone> CreateHabitMilestone(MileStoneSubType subType, Guid userId, Guid habitId, string value);
        public void CheckForGlobalMilestones(Guid userId);
        public void CheckForHabitMilestones(Guid userId, Guid habitId);
        public void CheckForGlobalCompletions(Guid userId, List<HabitLog> logs);
        public void CheckForGlobalPerfectDays(Guid userId, List<HabitLog> logs);
        public void CheckForGlobalFriendsSupported(Guid userId, List<Habit> habits);

        public void CheckForGlobalHabitStreak(Guid userId, List<HabitLog> logs);
    }
}
