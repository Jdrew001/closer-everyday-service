using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CED.Models.DTO
{
    public class DeviceDTO
    {
        public Guid DeviceId { get; set; }

        [FromHeader]
        public string Model { get; set; }

        [FromHeader]
        public string Platform { get; set; }

        [FromHeader]
        public string UUID { get; set; }

        [FromHeader]
        public string Manufacturer { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
