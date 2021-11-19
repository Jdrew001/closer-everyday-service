using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Models.Core
{
    public enum MileStoneScope
    {
        Global, Habit
    }

    public enum MileStoneSubType
    {
        Completion, Perfect, Streak, FriendsSupported, HabitsFormed, HabitsBroken
    }
    public class Milestone
    {
        public Guid Id { get; set; }
        public MilestoneType MilestoneType { get; set; }
        public Habit Habit { get; set; }
        public Guid UserId { get; set; }

        public string Value { get; set; }
    }
}
