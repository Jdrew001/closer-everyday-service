using CED.Data.Interfaces;
using CED.Models;
using CED.Models.Core;
using CED.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CED.Services.Core
{
    public class TokenService : ITokenService
    {
        private JwtToken _jwtToken;
        private readonly IBlacklistTokenRepository _blacklistTokenRepo;
        public TokenService(
            IOptions<JwtToken> jwtToken,
            IBlacklistTokenRepository blacklistTokenRepository)
        {
            _jwtToken = jwtToken.Value;
            _blacklistTokenRepo = blacklistTokenRepository;
        }
        public async Task<string> CreateJwtToken(User user)
        {
            return await Task.Run(() =>
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtToken.SecretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("uemail", user.Email),
                    new Claim("uid", user.Id.ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: _jwtToken.Issuer,
                    audience: _jwtToken.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtToken.TokenExpiry)),
                    notBefore: DateTime.Now.Subtract(TimeSpan.FromMinutes(30)),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            });
        }

        public Task<RefreshToken> CreateRefreshToken(Device device)
        {
            throw new NotImplementedException();
        }

        public async Task<JwtSecurityToken> ReadJwtToken(string token)
        {
            return await Task.Run(() =>
            {
                return new JwtSecurityTokenHandler().ReadJwtToken(token);
            });
        }

        public async Task<BlackListToken> FetchBlacklistedToken(string token)
        {
            return await _blacklistTokenRepo.GetToken(token);
        }
    }
}
