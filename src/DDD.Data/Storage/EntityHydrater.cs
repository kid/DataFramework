using System;
using System.Collections.Generic;
using System.Data;
using Castle.DynamicProxy;
using DDD.Data.MetaData;

namespace DDD.Data.Storage
{
    public class EntityHydrater
    {
        private readonly Session session;
        private readonly MetaDataStore metaDataStore;
        private readonly SessionLevelCache sessionLevelCache;
        private readonly ProxyGenerator proxyGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityHydrater"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="metaDataStore">The meta data store.</param>
        /// <param name="sessionLevelCache">The session level cache.</param>
        public EntityHydrater(
            Session session,
            MetaDataStore metaDataStore,
            SessionLevelCache sessionLevelCache)
        {
            this.session = session;
            this.metaDataStore = metaDataStore;
            this.sessionLevelCache = sessionLevelCache;
            this.proxyGenerator = new ProxyGenerator();
        }

        /// <summary>
        /// Hydrates the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public TEntity HydrateEntity<TEntity>(IDbCommand command)
        {
            IDictionary<string, object> values = null;

            using (var reader = command.ExecuteReader())
            {
                if (!reader.Read()) return default(TEntity);
                values = GetValuesFromCurrentRow(reader);
            }

            return CreateEntityFromValues<TEntity>(values);
        }

        /// <summary>
        /// Hydrates the entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> HydrateEntities<TEntity>(IDbCommand command)
        {
            var rows = new List<IDictionary<string, object>>();
            var entities = new List<TEntity>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    rows.Add(GetValuesFromCurrentRow(reader));
                }
            }

            foreach (var row in rows)
            {
                entities.Add(CreateEntityFromValues<TEntity>(row));
            }

            return entities;
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="command">The command.</param>
        public void UpdateEntity(Type type, object entity, IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                var tableInfo = metaDataStore.GetTableInfoFor(type);
                Hydrate(tableInfo, entity, GetValuesFromCurrentRow(reader));
            }
        }

        private IDictionary<string, object> GetValuesFromCurrentRow(IDataReader dataReader)
        {
            var values = new Dictionary<string, object>();

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                values.Add(dataReader.GetName(i), dataReader.GetValue(i));
            }

            return values;
        }

        private TEntity CreateEntityFromValues<TEntity>(IDictionary<string, object> values)
        {
            var tableInfo = metaDataStore.GetTableInfoFor<TEntity>();

            var cachedEntity = sessionLevelCache.TryToFind(typeof(TEntity), values[tableInfo.PrimaryKey.Name]);
            if (cachedEntity != null) return (TEntity)cachedEntity;

            var entity = Activator.CreateInstance<TEntity>();
            Hydrate(tableInfo, entity, values);
            sessionLevelCache.Store(typeof(TEntity), values[tableInfo.PrimaryKey.Name], entity);
            return entity;
        }

        private void Hydrate<TEntity>(TableInfo tableInfo, TEntity entity, IDictionary<string, object> values)
        {
            tableInfo.PrimaryKey.PropertyInfo.SetValue(entity, values[tableInfo.PrimaryKey.Name], null);
            SetRegularColumns(tableInfo, entity, values);
            SetReferenceProperties(tableInfo, entity, values);
        }

        private void SetRegularColumns<TEntity>(TableInfo tableInfo, TEntity entity, IDictionary<string, object> values)
        {
            foreach (var columnInfo in tableInfo.Columns)
            {
                if (columnInfo.PropertyInfo.CanWrite)
                {
                    object value = values[columnInfo.Name];
                    if (value is DBNull) value = null;
                    columnInfo.PropertyInfo.SetValue(entity, value, null);
                }
            }
        }

        private void SetReferenceProperties<TEntity>(TableInfo tableInfo, TEntity entity, IDictionary<string, object> values)
        {
            foreach (var referenceInfo in tableInfo.References)
            {
                if (referenceInfo.PropertyInfo.CanWrite)
                {
                    object foreignKeyValue = values[referenceInfo.Name];

                    if (foreignKeyValue is DBNull)
                    {
                        referenceInfo.PropertyInfo.SetValue(entity, null, null);
                    }
                    else
                    {
                        var referencedEntity = sessionLevelCache.TryToFind(referenceInfo.ReferenceType, foreignKeyValue) ??
                                               CreateProxy(tableInfo, referenceInfo, foreignKeyValue);

                        referenceInfo.PropertyInfo.SetValue(entity, referencedEntity, null);
                    }
                }
            }
        }

        private object CreateProxy(TableInfo tableInfo, ReferenceInfo referenceInfo, object foreignKeyValue)
        {
            var proxy = proxyGenerator.CreateClassProxy(referenceInfo.ReferenceType,
                new[] { new LazyLoadingInterceptor(tableInfo, session) });
            var referencePrimaryKey = metaDataStore.GetTableInfoFor(referenceInfo.ReferenceType).PrimaryKey;
            referencePrimaryKey.PropertyInfo.SetValue(proxy, foreignKeyValue, null);
            return proxy;
        }
    }
}
