using CED.Data.Interfaces;
using CED.Models.Core;
using CED.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Services.Core
{
    public class ReferenceService : IReferenceService
    {
        private readonly IReferenceRepository _referenceRepository;
        public ReferenceService(IReferenceRepository referenceRepository)
        {
            _referenceRepository = referenceRepository;
        }

        public async Task<List<HabitType>> GetHabitTypes()
        {
            return await _referenceRepository.GetHabitTypes();
        }

        public async Task<List<ScheduleType>> GetScheduleTypes()
        {
            return await _referenceRepository.GetScheduleTypes();
        }
    }
}
