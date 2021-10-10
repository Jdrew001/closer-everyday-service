using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Models.Core
{
    public class Schedule
    {
        public int Id { get; set; }
        public ScheduleType ScheduleType { get; set; }
        public int UserId { get; set; }
        public DateTime ScheduleTime { get; set; }
    }
}
