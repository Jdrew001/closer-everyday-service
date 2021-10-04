using MySql.Data.MySqlClient;
using System.Data;

namespace CED.Data
{
    class DataConnectionProvider
    {
        private MySqlConnection _sqlConnection;

        public MySqlConnection SqlConnection
        {
            get
            {
                return _sqlConnection;
            }
        }

        public DataConnectionProvider(string connectionString)
        {
            _sqlConnection = new MySqlConnection(connectionString);
            _sqlConnection.Open();
        }

        public MySqlCommand CreateCommand()
        {
            return _sqlConnection.CreateCommand();
        }

        public MySqlCommand CreateCommand(string storedProcedureName, MySqlTransaction transaction = null)
        {
            MySqlCommand command = _sqlConnection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = storedProcedureName;
            if (transaction != null)
                command.Transaction = transaction;
            return command;
        }

        public MySqlCommand CreateCommand(CommandType commandType, string commandText, MySqlTransaction transaction = null)
        {
            MySqlCommand command = _sqlConnection.CreateCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;
            if (transaction != null)
                command.Transaction = transaction;
            return command;
        }

        public void Dispose()
        {
            if (_sqlConnection == null)
                return;
            _sqlConnection.Close();
            _sqlConnection.Dispose();
            _sqlConnection = null;
        }
    }
}
