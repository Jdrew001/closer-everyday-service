using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Models.Core
{
    public class Milestone
    {
        public Guid Id { get; set; }
        public MilestoneType MilestoneType { get; set; }
        public Habit Habit { get; set; }
        public Guid UserId { get; set; }
    }
}
