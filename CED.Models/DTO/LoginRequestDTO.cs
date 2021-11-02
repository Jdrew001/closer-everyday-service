using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CED.Models.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        [EmailAddress]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }

        [Required]
        [JsonProperty("deviceUUID")]
        public string DeviceUUID { get; set; }

        [JsonIgnore]
        public string IpAddress { get; set; }
    }
}
