using System.Data;
using System.Threading.Tasks;
using CED.Data.Interfaces;
using CED.Models;
using Microsoft.Extensions.Options;

namespace CED.Data.Repositories
{
    public class TemplateRepository : DataProvider, ITemplateRepository
    {
        public TemplateRepository(
            IOptions<ConnectionStrings> connectionStrings)
            : base(connectionStrings.Value.CEDDB) 
        {
        }

        public async Task<string> GetTemplateByKey(string key)
        {
            string result = null;
            string spName = "GetEmailTemplate";
            using DataConnectionProvider dcp = CreateConnection();
            await using var command = dcp.CreateCommand(spName);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Key", key);

            using DataReaderHelper drh = await command.ExecuteReaderAsync();

            while(drh.Read())
                result = drh.Get<string>("templateId");

            return result;
        }
    }
}