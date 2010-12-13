using System;
using System.Data;
using DDD.Data.MetaData;

namespace DDD.Data.Storage
{
    public class InitializeProxyAction : DatabaseAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InitializeProxyAction"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="metaDataStore">The meta data store.</param>
        /// <param name="hydrater">The entity hydrater.</param>
        /// <param name="sessionLevelCache">The session level cache.</param>
        public InitializeProxyAction(
            IDbConnection connection,
            IDbTransaction transaction,
            MetaDataStore metaDataStore,
            EntityHydrater hydrater,
            SessionLevelCache sessionLevelCache)
            : base(connection, transaction, metaDataStore, hydrater, sessionLevelCache)
        {
        }

        public void InitializeProxy(object proxy, Type targetType)
        {
            using (var command = CreateCommand())
            {
                var tableInfo = MetaDataStore.GetTableInfoFor(targetType);
                //var query = tableInfo.GetSelectStatementForAllFields();
                //tableInfo.AddWhereByIdClause(query);

                //object id = tableInfo.PrimaryKey.PropertyInfo.GetValue(proxy, null);
                //command.CommandText = query.ToString();
                //command.CreateAndAddInputParameter(tableInfo.PrimaryKey.DbType, tableInfo.GetPrimaryKeyParameterName(), id);

                Hydrater.UpdateEntity(targetType, proxy, command);
            }
        }
    }
}
