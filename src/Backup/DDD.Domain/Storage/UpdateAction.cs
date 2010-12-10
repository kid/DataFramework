using System.Data;
using DDD.Domain.MetaData;

namespace DDD.Domain.Storage
{
    public class UpdateAction : DatabaseAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAction"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="metaDataStore">The meta data store.</param>
        /// <param name="hydrater">The entity hydrater.</param>
        /// <param name="sessionLevelCache">The session level cache.</param>
        public UpdateAction(
            IDbConnection connection,
            IDbTransaction transaction,
            MetaDataStore metaDataStore,
            EntityHydrater hydrater,
            SessionLevelCache sessionLevelCache)
            : base(connection, transaction, metaDataStore, hydrater, sessionLevelCache)
        {
        }

        public TEntity Update<TEntity>(TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
