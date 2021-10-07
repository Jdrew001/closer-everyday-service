using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CED.Models.DTO
{
    public class DeviceDTO
    {
        public int DeviceId { get; set; }
        public string Model { get; set; }
        public string Platform { get; set; }
        public string UUID { get; set; }
        public string Manufacturer { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
    }
}
