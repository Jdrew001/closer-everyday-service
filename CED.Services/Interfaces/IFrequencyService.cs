using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CED.Models.Core;

namespace CED.Services.Interfaces
{
    public interface IFrequencyService
    {
        public Task<Frequency> GetHabitFrequency(Guid habitId);
        public Task<Frequency> SaveHabitFrequency(Frequency frequency, Guid habitId);
        public Task<Frequency> ClearHabitFrequency(Guid habitId);
        public Task<List<Day>> GetFrequencyDays(Guid frequencyId);
    }
}
