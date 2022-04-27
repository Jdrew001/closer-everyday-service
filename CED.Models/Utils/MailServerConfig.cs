using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CED.Models.Utils
{
    public class MailServerConfig
    {
        public string SendEmail { get; set; }
        public string FriendlyName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool RouteAllEmail { get; set; }
        public List<string> ForwardingAddresses { get; set; }
    }
}