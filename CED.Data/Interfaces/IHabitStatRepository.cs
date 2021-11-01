using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Data.Interfaces
{
    public interface IHabitStatRepository
    {
        public Task<double> GetGlobalSuccessRate(Guid userId);
        public Task<int> GetFriendStat(Guid userId);
    }
}
