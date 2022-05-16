using CED.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Data.Interfaces
{
    public interface IRefreshTokenRepository
    {
        public Task<RefreshToken> GetRefreshToken(string token);
        public Task<List<RefreshToken>> GetUserRefreshTokens(Guid userId);
        public Task SaveRefreshToken(RefreshToken token, Guid userId);
        public Task<RefreshToken> DeleteRefreshToken(string token);
    }
}
