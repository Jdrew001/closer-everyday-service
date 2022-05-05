using CED.Models;
using CED.Models.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace CED.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateJwtToken(User user);
        Task<JwtSecurityToken> ReadJwtToken(string token);
        Task<RefreshToken> CreateRefreshToken(Device device);
        Task<BlackListToken> FetchBlacklistedToken(string token);
    }
}
