using System;
using System.Data;
using DDD.Domain.MetaData;

namespace DDD.Domain
{
    public interface ISession : IDisposable
    {
        void Commit();
        void Rollback();

        TEntity Get<TEntity>(object id);

        TEntity Insert<TEntity>(TEntity entity);
        TEntity Update<TEntity>(TEntity entity);
        void Delete<TEntity>(TEntity entity);

        TableInfo GetTableInfoFor<TEntity>();

        void ClearCache();
        void RemoveFromCache(object entity);
        void RemoveAllInstancesFromCache<TEntity>();

        IDbConnection GetConnection();
        IDbTransaction GetTransaction();
    }
}
