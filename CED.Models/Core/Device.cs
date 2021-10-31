using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Models.Core
{
    public class Device
    {
        public Guid DeviceId { get; set; }
        public string Model { get; set; }
        public string Platform { get; set; }
        public string UUID { get; set; }
        public string Manufacturer { get; set; }
        public User User { get; set; }
    }
}
