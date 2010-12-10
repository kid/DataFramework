using System.Data;
using DDD.Domain.MetaData;

namespace DDD.Domain.Storage
{
    public class DeleteAction : DatabaseAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAction"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="metaDataStore">The meta data store.</param>
        /// <param name="hydrater">The entity hydrater.</param>
        /// <param name="sessionLevelCache">The session level cache.</param>
        public DeleteAction(
            IDbConnection connection,
            IDbTransaction transaction,
            MetaDataStore metaDataStore,
            EntityHydrater hydrater,
            SessionLevelCache sessionLevelCache)
            : base(connection, transaction, metaDataStore, hydrater, sessionLevelCache)
        {
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        public void Delete<TEntity>(TEntity entity)
        {
            using (var command = CreateCommand())
            {
                var tableInfo = MetaDataStore.GetTableInfoFor<TEntity>();

                command.CommandText = tableInfo.GetDeleteStoredProcName();
                command.CreateAndAddInputParameter(
                    tableInfo.PrimaryKey.Name,
                    tableInfo.PrimaryKey.DbType,
                    tableInfo.PrimaryKey.PropertyInfo.GetValue(entity, null)
                );
                command.ExecuteNonQuery();

                SessionLevelCache.Remove(entity);
            }
        }
    }
}
