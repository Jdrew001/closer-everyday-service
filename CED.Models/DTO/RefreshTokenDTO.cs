using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CED.Models.DTO
{
    public class RefreshTokenDTO
    {
        [Required]
        [MaxLength(256)]
        public string Token { get; set; }

        [JsonIgnore]
        public string IpAddress { get; set; }

        [JsonIgnore]
        public string DeviceUUID { get; set; }
    }
}
