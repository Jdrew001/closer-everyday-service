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
    public class FrequencyService : IFrequencyService
    {
        private readonly IFrequencyRepository _frequencyRepository;

        public FrequencyService(IFrequencyRepository frequencyRepository)
        {
            _frequencyRepository = frequencyRepository;
        }

        public async Task<Frequency> GetHabitFrequency(Guid habitId)
        {
            Frequency frequency =  await _frequencyRepository.GetHabitFrequency(habitId);
            frequency.Days = await GetFrequencyDays(frequency.Id);
            return frequency;
        }

        public async Task<Frequency> SaveHabitFrequency(Frequency frequency, Guid habitId)
        {
            return await _frequencyRepository.SaveHabitFrequency(frequency.Id, habitId);
        }

        public async Task<Frequency> ClearHabitFrequency(Guid habitId)
        {
            return await _frequencyRepository.ClearHabitFrequency(habitId);
        }

        public async Task<List<Day>> GetFrequencyDays(Guid frequencyId)
        {
            return await _frequencyRepository.GetFrequencyDays(frequencyId);
        }
    }
}
