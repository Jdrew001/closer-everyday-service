using CED.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Services.Interfaces
{
    public interface IReferenceService
    {
        //habit type
        //schedule type
        Task<List<HabitType>> GetHabitTypes();
        Task<List<ScheduleType>> GetScheduleTypes();
    }
}
