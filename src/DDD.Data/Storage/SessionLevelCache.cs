﻿using System;
using System.Collections.Generic;

namespace DDD.Data.Storage
{
    public class SessionLevelCache
    {
        private readonly Dictionary<Type, Dictionary<string, object>> cache = new Dictionary<Type, Dictionary<string, object>>();

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
            var type = entity.GetType();

            if (!cache.ContainsKey(type)) return;

            string keyToRemove = null;

            foreach (var pair in cache[type])
            {
                if (pair.Value == entity)
                {
                    keyToRemove = pair.Key;
                }
            }

            if (keyToRemove != null)
            {
                cache[type].Remove(keyToRemove);
            }
        }

        internal void ClearAll()
        {
            this.cache.Clear();
        }

        internal void RemoveAllInstancesOf(Type type)
        {
            if (cache.ContainsKey(type))
            {
                cache.Remove(type);
            }
        }

        internal void Store(Type type, object id, object entity)
        {
            if (!cache.ContainsKey(type)) cache.Add(type, new Dictionary<string, object>());
            cache[type][id.ToString()] = entity;
        }
    }
}
