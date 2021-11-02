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
        public Task<List<Frequency>> GetAllFrequencies();
        public Task<Frequency> GetFrequencyById(Guid id);
        public Task<List<Frequency>> GetHabitFrequencies(Guid habitId);
        public Task<List<Frequency>> SaveHabitFrequencies(List<Frequency> frequencies, Guid habitId);
        public Task<List<Frequency>> UpdateHabitFrequencies(List<Frequency> frequencies, Guid habitId);
    }
}
