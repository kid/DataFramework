using System.Data;
using DDD.Data.MetaData;

namespace DDD.Data.Storage
{
    public class GetByIdAction<TEntity> : DatabaseAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetByIdAction"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="metaDataStore">The meta data store.</param>
        /// <param name="hydrater">The entity hydrader.</param>
        /// <param name="sessionLevelCache">The session level cache.</param>
        public GetByIdAction(
            IDbConnection connection,
            IDbTransaction transaction,
            MetaDataStore metaDataStore,
            EntityHydrater hydrater,
            SessionLevelCache sessionLevelCache)
            : base(connection, transaction, metaDataStore, hydrater, sessionLevelCache)
        {
        }

        /// <summary>
        /// Gets the entity corresponding to the specified id.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public TEntity Get(object id)
        {
            TEntity cachedEntity;
            if (this.SessionLevelCache.TryToFind<TEntity>(id, out cachedEntity))
            {
                return cachedEntity;
            }

            using (var command = this.CreateCommand())
            {
                var tableInfo = this.MetaDataStore.GetTableInfoFor<TEntity>();


                command.CommandText = this.MetaDataStore.GetStoredProcNameFor<GetByIdAction<TEntity>>();
                command.CreateAndAddInputParameter(
                    tableInfo.PrimaryKey.Name,
                    tableInfo.PrimaryKey.DbType,
                    id
                );

                return this.Hydrater.HydrateEntity<TEntity>(command);
            }
        }
    }
}
