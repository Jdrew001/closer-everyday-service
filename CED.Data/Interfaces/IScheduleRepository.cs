using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Models.Core;

namespace CED.Data.Interfaces
{
    public interface IScheduleRepository
    {
        Task<Schedule> GetScheduleByHabitId(Guid habitId);
        Task<Schedule> SaveSchedule(Schedule schedule);
        Task<Schedule> UpdateSchedule(Schedule schedule);
    }
}
