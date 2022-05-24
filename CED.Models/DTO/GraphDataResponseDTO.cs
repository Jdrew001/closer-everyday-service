using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CED.Models.DTO
{
    public class GraphDataResponseDTO
    {
        public string SubTitle { get; set; }
        
        public string StartDate { get; set; }
        
        public string EndDate { get; set; }
        
        public List<GraphDataDTO> GraphData { get; set; }
        
        public bool Selected { get; set; }
        
        public string DefaultSelected { get; set; }
    }
}