using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CED.Models.Core
{
    public class BlackListToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}