using System;

namespace DDD.Domain.Storage
{
    public class SessionLevelCache
    {
        public object TryToFind(Type type, object id)
        {
            return null;
        }

        public bool TryToFind<TEntity>(object id, out TEntity entity)
        {
            entity = default(TEntity);
            return false;
        }

        public void Remove(object entity)
        {
        }

        internal void ClearAll()
        {
            throw new NotImplementedException();
        }

        internal void RemoveAllInstancesOf(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
