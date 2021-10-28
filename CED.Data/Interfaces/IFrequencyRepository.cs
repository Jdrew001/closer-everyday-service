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
        Task<Frequency> GetFrequencyById(int id);
        Task<List<Frequency>> GetHabitFrequencies(int habitId);

        Task<Frequency> SaveHabitFrequency(int frequencyId, int habitId);
    }
}
