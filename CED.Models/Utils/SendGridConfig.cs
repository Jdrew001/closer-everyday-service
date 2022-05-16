using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CED.Models.Utils
{
    public class SendGridConfig
    {
        public string ApiToken { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}