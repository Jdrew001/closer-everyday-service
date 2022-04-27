using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CED.Models.Core
{
    public class AuthCode
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public Guid UserId { get; set; }
    }
}