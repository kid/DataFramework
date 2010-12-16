using System.Data;
using DDD.Data.MetaData;

namespace DDD.Data.Storage
{
    public class DeleteAction<TEntity> : DatabaseAction
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
        public void Delete(TEntity entity)
        {
            using (var command = this.CreateCommand())
            {
                var tableInfo = this.MetaDataStore.GetTableInfoFor<TEntity>();

                command.CommandText = this.MetaDataStore.GetStoredProcNameFor<DeleteAction<TEntity>>();
                command.CreateAndAddInputParameter(
                    tableInfo.PrimaryKey.Name,
                    tableInfo.PrimaryKey.DbType,
                    tableInfo.PrimaryKey.PropertyInfo.GetValue(entity, null)
                );
                command.ExecuteNonQuery();

                this.SessionLevelCache.Remove(entity);
            }
        }
    }
}
