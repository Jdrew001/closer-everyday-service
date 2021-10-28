using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Data.Interfaces;
using CED.Models.Core;
using CED.Services.Interfaces;

namespace CED.Services.Core
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(
            IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<Schedule> GetScheduleByHabitId(int habitId)
        {
            return await _scheduleRepository.GetScheduleByHabitId(habitId);
        }

        public async Task<Schedule> SaveSchedule(Schedule schedule)
        {
            return await _scheduleRepository.SaveSchedule(schedule);
        }

        public async Task<Schedule> UpdateSchedule(Schedule schedule)
        {
            return await _scheduleRepository.UpdateSchedule(schedule);
        }
    }
}
