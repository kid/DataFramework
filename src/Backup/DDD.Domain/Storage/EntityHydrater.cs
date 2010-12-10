using System;
using System.Data;
using DDD.Domain.MetaData;

namespace DDD.Domain.Storage
{
    public class EntityHydrater
    {
        public EntityHydrater(
            ISession session,
            MetaDataStore metaDataStore,
            SessionLevelCache sessionLevelCache)
        {

        }

        public TEntity HydrateEntity<TEntity>(IDbCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
