using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Models.DTO
{
    public class GenericResponseDTO
    {
        public string message { get; set; }
        public string status { get; set; }
        public Object data { get; set; }
        public bool Error { get; set; }
    }
}
