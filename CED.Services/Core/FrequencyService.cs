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
        public async Task<List<Frequency>> GetAllFrequencies()
        {
            return await _frequencyRepository.GetAllFrequencies();
        }

        public async Task<Frequency> GetFrequencyById(Guid id)
        {
            return await _frequencyRepository.GetFrequencyById(id);
        }

        public async Task<List<Frequency>> GetHabitFrequencies(Guid habitId)
        {
            return await _frequencyRepository.GetHabitFrequencies(habitId);
        }

        public async Task<List<Frequency>> SaveHabitFrequencies(List<Frequency> frequencies, Guid habitId)
        {
            var savedFrequencies = new List<Frequency>();
            for (int i = 0; i < frequencies.Count; i++)
            {
                var freq = await _frequencyRepository.SaveHabitFrequency(frequencies[i].Id, habitId);
                savedFrequencies.Add(freq);
            }

            return savedFrequencies;
        }
    }
}
