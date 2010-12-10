using System.Data;
using DDD.Domain.MetaData;

namespace DDD.Domain.Storage
{
    public abstract class DatabaseAction
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction transaction;
        private readonly MetaDataStore metaDataStore;
        private readonly EntityHydrater hydrater;
        private readonly SessionLevelCache sessionLevelCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseAction"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="metaDataStore">The meta data store.</param>
        /// <param name="entityHydrater">The entity hydrater.</param>
        /// <param name="sessionLevelCache">The session level cache.</param>
        protected DatabaseAction(
            IDbConnection connection,
            IDbTransaction transaction,
            MetaDataStore metaDataStore,
            EntityHydrater hydrater,
            SessionLevelCache sessionLevelCache)
        {
            this.connection = connection;
            this.transaction = transaction;
            this.metaDataStore = metaDataStore;
            this.hydrater = hydrater;
            this.sessionLevelCache = sessionLevelCache;
        }

        /// <summary>
        /// Gets the meta data store.
        /// </summary>
        /// <value>The meta data store.</value>
        protected MetaDataStore MetaDataStore { get { return this.metaDataStore; } }

        /// <summary>
        /// Gets the entity hydrater.
        /// </summary>
        /// <value>The entity hydrater.</value>
        protected EntityHydrater Hydrater { get { return this.hydrater; } }

        /// <summary>
        /// Gets the session level cache.
        /// </summary>
        /// <value>The session level cache.</value>
        protected SessionLevelCache SessionLevelCache { get { return this.sessionLevelCache; } }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <returns></returns>
        protected IDbCommand CreateCommand()
        {
            var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandType = CommandType.StoredProcedure;

            command.CreateAndAddInputParameter("header", DbType.String, "");
            command.CreateAndAddOuputParameter("return_code", DbType.StringFixedLength);
            command.CreateAndAddOuputParameter("sql_code", DbType.Int32);

            return command;
        }
    }
}
