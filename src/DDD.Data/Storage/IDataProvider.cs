using System.Data;

namespace DDD.Data.Storage
{
    public interface IDataProvider
    {
        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        IDbConnection CreateConnection();
    }
}
