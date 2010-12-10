using System;
using System.Data;

namespace DDD.Domain.Storage
{
    public static class TypeConverter
    {
        /// <summary>
        /// Convert to specified type to the corresponding type in the database.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static DbType ToDbType(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
