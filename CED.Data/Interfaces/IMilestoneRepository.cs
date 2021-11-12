using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Models.Core;

namespace CED.Data.Interfaces
{
    public interface IMilestoneRepository
    {
        Task<MilestoneType> GetMilestoneType(MileStoneScope type, MileStoneSubType subType);
        Task<Milestone> CreateGlobalMilestone(int milestoneTypeId, Guid userId, string value);
        Task<Milestone> CreateHabitMilestone(int milestoneTypeId, Guid userId, Guid habitId, string value);
        Task<Milestone> GetMilestoneByType(MileStoneScope type, MileStoneSubType subType, string value); 
    }
}
