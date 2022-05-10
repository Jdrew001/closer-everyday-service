using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CED.Models.DTO
{
    public class ValidateCodeDTO
    {
        public string Code { get; set; }
        
        public string Email { get; set; }
        public string ValidationType { get; set; }
    }
}