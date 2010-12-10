using System;
using System.Data;
using DDD.Data.MetaData;

namespace DDD.Data.Storage
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
