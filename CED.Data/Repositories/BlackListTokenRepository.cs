using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CED.Data.Interfaces;
using CED.Models;
using CED.Models.Core;
using Microsoft.Extensions.Options;

namespace CED.Data.Repositories
{
  public class BlackListTokenRepository : DataProvider, IBlacklistTokenRepository
  {
    protected BlackListTokenRepository(IOptions<ConnectionStrings> connectionStrings)
        : base(connectionStrings.Value.CEDDB)
    {
    }

    public async Task<BlackListToken> GetToken(string token)
    {
        string spName = "CheckForTokenInBlacklist";
        BlackListToken blackListToken = null;
        using DataConnectionProvider dcp = CreateConnection();
        await using var command = dcp.CreateCommand(spName);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("appToken", token);

        using DataReaderHelper drh = await command.ExecuteReaderAsync();
        while (drh.Read()) 
        {
            blackListToken = new BlackListToken()
            {
                Id = new Guid(drh.Get<string>("id")),
                Token = drh.Get<string>("token"),
                Expiry = drh.Get<DateTime>("expiry")
            };
        }

        return blackListToken; 
    }
  }
}