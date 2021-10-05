using CED.Models.Core;
using CED.Models.DTO;
using System.Threading.Tasks;

namespace CED.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationDTO> Login(LoginRequestDTO loginRequestDto);
        Task<AuthenticationDTO> RefreshToken(string token);
        Task Logout(string token);
    }
}
