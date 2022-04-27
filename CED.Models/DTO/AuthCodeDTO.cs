using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CED.Models.DTO
{
    public class AuthCodeDTO
    {
        public string Code { get; set; }
        public Guid UserId { get; set; }
    }
}