using System.Data;

namespace Stag.Storage
{
    public interface IConnectionManager
    {
        IDbConnection CreateConnection();

        IDataParameter CreateParameter(string parameterName, object value);
    }
}