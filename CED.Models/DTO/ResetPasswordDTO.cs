using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CED.Models.DTO
{
    public class ResetPasswordDTO
    {
        public Guid UserId { get; set; }
        
        public string Password { get; set; }
        
        
    }
}