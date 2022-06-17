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
        Task<Frequency> GetHabitFrequency(Guid habitId);

        Task<Frequency> SaveHabitFrequency(Guid frequencyId, Guid habitId);

        Task<Frequency> ClearHabitFrequency(Guid habitId);

        Task<List<Day>> GetFrequencyDays(Guid frequencyId);
    }
}
