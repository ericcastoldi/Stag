using System.Data;
using System.Data.SqlClient;

namespace Stag.Storage
{
    public class HelpnetConnectionManager : IConnectionManager
    {
        private const string ConnectionString = "Server=server;User Id=user;Password=pwd;Initial Catalog=database;";

        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            return connection;
        }

        public IDataParameter CreateParameter(string parameterName, object value)
        {
            return new SqlParameter(parameterName, value);
        }
    }
}