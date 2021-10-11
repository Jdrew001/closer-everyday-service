using CED.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Models.DTO
{
    public class ReferenceDTO
    {
        public List<HabitType> habitTypes { get; set; }
        public List<ScheduleType> scheduleTypes { get; set; }
    }
}
