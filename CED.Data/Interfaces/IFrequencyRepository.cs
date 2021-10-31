using CED.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Data.Interfaces
{
    public interface IFrequencyRepository
    {
        Task<List<Frequency>> GetAllFrequencies();
        Task<Frequency> GetFrequencyById(Guid id);
        Task<List<Frequency>> GetHabitFrequencies(Guid habitId);

        Task<Frequency> SaveHabitFrequency(Guid frequencyId, Guid habitId);
    }
}
