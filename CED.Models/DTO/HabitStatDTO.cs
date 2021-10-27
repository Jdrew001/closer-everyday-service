using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Models.DTO
{
    public class HabitStatDTO
    {
        public int currentStreak { get; set; }
        public int maxStreak { get; set; }
        public double averageSuccessReate { get; set; }
        public Dictionary<string, double> monthlySuccessRate { get; set; }
        public int perfectDays { get; set; }
        public int totalFriendsHelping { get; set; }
        public int totalCompletions { get; set; }
    }
}
