using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CED.Models.Utils
{
    public class SendGridConfig
    {
        public string ApiToken { get; set; }
        public string DevEmail { get; set; }
        public string ProdEmail { get; set; }
    }
}