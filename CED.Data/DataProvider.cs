using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Data
{
    class DataProvider
    {
        protected string ConnectionStringName { get; set; }

        protected DataConnectionProvider CreateConnection()
        {
            return new DataConnectionProvider(this.ConnectionStringName);
        }

        protected DataProvider(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
        }
    }
}
