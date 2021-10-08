using System;

namespace CED.Models.Core
{
    public class AuthenticationDTO
    {
        public bool IsAuthenticated { get; set; }
        public Int32 UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public string Message { get; set; }
        public bool IsNewDevice { get; set; }
    }
}
