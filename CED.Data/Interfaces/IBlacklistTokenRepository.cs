using System.Threading.Tasks;
using CED.Models.Core;

namespace CED.Data.Interfaces
{
  public interface IBlacklistTokenRepository
    {
         Task<BlackListToken> GetToken(string token);
    }
}