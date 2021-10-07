using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CED.Models.DTO
{
    public class RegistrationDTO
    {
        [MaxLength(100)]
        [Required]
        public string firstName { get; set; }

        [MaxLength(100)]
        [Required]
        public string lastName { get; set; }

        [MaxLength(256)]
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [MaxLength(100)]
        [Required]
        public string password { get; set; }

        [Required]
        [MaxLength(100)]
        public string deviceGuid { get; set; }

        [Required]
        [MaxLength(100)]
        public string deviceModel { get; set; }

        [Required]
        [MaxLength(100)]
        public string devicePlatform { get; set; }

        [Required]
        [MaxLength(100)]
        public string deviceManufacture { get; set; }

        [JsonIgnore]
        public string IpAddress { get; set; }
    }
}
