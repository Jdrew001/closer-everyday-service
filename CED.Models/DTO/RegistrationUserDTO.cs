using System;

namespace CED.Models.DTO
{
  public class RegistrationUserDTO
    {
        public bool IsUserCreated { get; set; }
        public string Message { get; set; }
        public bool ShouldRedirectToLogin { get; set; }
        public Guid UserId { get; set; }
    }
}