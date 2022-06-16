using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CED.Models.DTO
{
    public class SwipeDashboardGraphDTO
    {
        public int Limit { get; set; }
        
        public string BoundaryStartDate { get; set; }
        
        public string BoundaryEndDate { get; set; }
        
        public Boundary Boundary { get; set; }
    }

    public enum Boundary {
        first,
        last
    }
}