using System;
using System.Collections.Generic;
using System.Data;

namespace DDD.Data.Storage
{
    public static class TypeConverter
    {
        private static readonly Dictionary<Type, DbType> typeToDbType = new Dictionary<Type, DbType>
        {
            { typeof(string), DbType.String },
            { typeof(DateTime), DbType.DateTime },
            { typeof(DateTime?), DbType.DateTime },
            { typeof(int), DbType.Int32 },
            { typeof(int?), DbType.Int32 },
            { typeof(long), DbType.Int64 },
            { typeof(long?), DbType.Int64 },
            { typeof(bool), DbType.Boolean },
            { typeof(bool?), DbType.Boolean },
            { typeof(byte[]), DbType.Binary },
            { typeof(decimal), DbType.Decimal },
            { typeof(decimal?), DbType.Decimal },
            { typeof(double), DbType.Double },
            { typeof(double?), DbType.Double },
            { typeof(float), DbType.Single },
            { typeof(float?), DbType.Single },
            { typeof(Guid), DbType.Guid },
            { typeof(Guid?), DbType.Guid }
        };

        /// <summary>
        /// Convert to specified type to the corresponding type in the database.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static DbType ToDbType(Type type)
        {
            DbType dbType;
            if (!typeToDbType.TryGetValue(type, out dbType))
            {
                throw new InvalidOperationException(string.Format("Type {0} doesn't have a matching DbType configured", type.FullName));
            }
            return dbType;
        }
    }
}
