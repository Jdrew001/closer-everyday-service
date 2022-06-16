using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CED.Models.DTO
{
    public class InitDashboardGraphDTO
    {
        public string CurrentDate { get; set; }
        
        public int Limit { get; set; }
    }
}