using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CED.Models.DTO
{
    public class EmailForReset
    {
        public bool IsUser { get; set; }
        public string Message { get; set; }
        public Guid UserId { get; set; }
    }
}