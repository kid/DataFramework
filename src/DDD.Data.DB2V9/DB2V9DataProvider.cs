using System.Configuration;
using System.Data;
using DDD.Data.Storage;
using MutSocMut.Framework.Data;

namespace DDD.Data.DB2V9
{
    public class DB2V9DataProvider : IDataProvider
    {
        private readonly string db2Ref;

        /// <summary>
        /// Initializes a new instance of the <see cref="DB2V9DataProvider"/> class.
        /// </summary>
        public DB2V9DataProvider()
            : this(ConfigurationManager.AppSettings["DB2Ref"])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DB2V9DataProvider"/> class.
        /// </summary>
        /// <param name="db2Ref">The DB2 ref.</param>
        public DB2V9DataProvider(string db2Ref)
        {
            this.db2Ref = db2Ref;
        }

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            return DB2Connections.Create(db2Ref);
        }
    }
}
