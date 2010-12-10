using System;
using System.Data;
using System.Reflection;
using DDD.Data.Storage;

namespace DDD.Data.MetaData
{
    public class ColumnInfo : MetaData
    {
        private readonly string name;
        private readonly Type dotNetType;
        private readonly DbType dbType;
        private readonly PropertyInfo propertyInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnInfo"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="name">The name.</param>
        /// <param name="dotNetType">Type of the dot net.</param>
        /// <param name="propertyInfo">The property info.</param>
        public ColumnInfo(MetaDataStore store, string name, Type dotNetType, PropertyInfo propertyInfo)
            : this(store, name, dotNetType, TypeConverter.ToDbType(dotNetType), propertyInfo)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnInfo"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="name">The name.</param>
        /// <param name="dotNetType">Type of the dot net property.</param>
        /// <param name="dbType">Type of the db.</param>
        /// <param name="propertyInfo">The property info.</param>
        public ColumnInfo(MetaDataStore store, string name, Type dotNetType, DbType dbType, PropertyInfo propertyInfo)
            : base(store)
        {
            this.name = name;
            this.dotNetType = dotNetType;
            this.dbType = dbType;
            this.propertyInfo = propertyInfo;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get { return this.name; } }

        /// <summary>
        /// Gets the type of the property.
        /// </summary>
        /// <value>The type of the dot net.</value>
        public Type DotNetType { get { return this.dotNetType; } }

        /// <summary>
        /// Gets the corresponding database type.
        /// </summary>
        /// <value>The type of the db.</value>
        public DbType DbType { get { return this.dbType; } }

        /// <summary>
        /// Gets the property info.
        /// </summary>
        /// <value>The property info.</value>
        public PropertyInfo PropertyInfo { get { return this.propertyInfo; } }
    }
}
