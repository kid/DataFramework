using System;
using System.Data;
using DDD.Data.MetaData;
using DDD.Data.Storage;

namespace DDD.Data
{
    public class Session : ISession
    {
        private IDbConnection connection;
        private IDbTransaction transaction;
        private readonly IDataProvider dataProvider;
        private readonly MetaDataStore metaDataStore;
        private readonly EntityHydrater hydrater;
        private readonly SessionLevelCache sessionLevelCache;

        public Session(IDataProvider dataProvider, MetaDataStore metaDataStore)
        {
            this.dataProvider = dataProvider;
            this.metaDataStore = metaDataStore;
            sessionLevelCache = new SessionLevelCache();
            hydrater = new EntityHydrater(this, metaDataStore, sessionLevelCache);
        }

        private void InitializeConnection()
        {
            this.connection = this.dataProvider.CreateConnection();
            this.connection.Open();
            this.transaction = this.connection.BeginTransaction();
        }

        public IDbConnection GetConnection()
        {
            if (connection == null)
            {
                InitializeConnection();
            }

            return connection;
        }

        public IDbTransaction GetTransaction()
        {
            if (transaction == null)
            {
                InitializeConnection();
            }

            return transaction;
        }

        public TableInfo GetTableInfoFor<TEntity>()
        {
            return metaDataStore.GetTableInfoFor<TEntity>();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        public void Dispose()
        {
            if (transaction != null) transaction.Dispose();
            if (connection != null) connection.Dispose();
        }

        private TAction CreateAction<TAction>()
            where TAction : DatabaseAction
        {
            return (TAction)Activator.CreateInstance(typeof(TAction), GetConnection(), GetTransaction(),
                metaDataStore, hydrater, sessionLevelCache);
        }

        public TEntity Get<TEntity>(object id)
        {
            return CreateAction<GetByIdAction>().Get<TEntity>(id);
        }

        public TEntity Insert<TEntity>(TEntity entity)
        {
            return CreateAction<InsertAction>().Insert(entity);
        }

        public TEntity Update<TEntity>(TEntity entity)
        {
            return CreateAction<UpdateAction>().Update(entity);
        }

        public void Delete<TEntity>(TEntity entity)
        {
            CreateAction<DeleteAction>().Delete(entity);
        }

        public void InitializeProxy(object proxy, Type targetType)
        {
            CreateAction<InitializeProxyAction>().InitializeProxy(proxy, targetType);
        }

        public void ClearCache()
        {
            sessionLevelCache.ClearAll();
        }

        public void RemoveFromCache(object entity)
        {
            sessionLevelCache.Remove(entity);
        }

        public void RemoveAllInstancesFromCache<TEntity>()
        {
            sessionLevelCache.RemoveAllInstancesOf(typeof(TEntity));
        }
    }
}
