using CED.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CED.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public string PasswordSalt { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool Locked { get; set; }
        public DateTime? DateLocked { get; set; }
        public string Token { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
