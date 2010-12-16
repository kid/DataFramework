using System;
using System.Collections.Generic;
using DDD.Data.MetaData;

namespace DDD.Data.DB2V9
{
    public class CataloguedStoredProcNamingStrategy : IStoredProcNamingStrategy
    {
        private readonly IDictionary<Type, string> catalog = new Dictionary<Type, string>();

        public string GetStoredProcNameFor<TAction>()
        {
            string name;
            if (!this.catalog.TryGetValue(typeof(TAction), out name))
            {
                throw new InvalidOperationException(string.Format(
                    "No stored proc name registered for action {0}.", typeof(TAction)
                ));
            }
            return this.catalog[typeof(TAction)];
        }

        public void SetStoredProcName<TAction>(string storedProcName)
        {
            var type = typeof(TAction);
            if (this.catalog.ContainsKey(type))
            {
                throw new InvalidOperationException("This action has already been registred.");
            }
            this.catalog[type] = storedProcName;
        }
    }
}
