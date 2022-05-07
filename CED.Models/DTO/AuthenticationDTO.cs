using System;

namespace CED.Models.Core
{
    public class AuthenticationDTO
    {
        public bool IsAuthenticated { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public string Message { get; set; }
        public bool IsNewDevice { get; set; }
        public bool ShouldRedirectoToLogin { get; set; }
        public bool Error { get; set; }
        public bool Confirmed { get; set; } = true;
        public string Firstname { get; set; }
        public string Lastname {get; set; }
    }
}
