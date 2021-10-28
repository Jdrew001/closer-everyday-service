using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Models.Core;

namespace CED.Services.Interfaces
{
    public interface IScheduleService
    {
        public Task<Schedule> GetScheduleByHabitId(int habitId);
        public Task<Schedule> SaveSchedule(Schedule schedule);
        public Task<Schedule> UpdateSchedule(Schedule schedule);
    }
}
