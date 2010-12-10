using System;
using System.Reflection;

namespace DDD.Domain.MetaData
{
    public class ReferenceInfo : ColumnInfo
    {
        private readonly Type referenceType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceInfo"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="name">The name.</param>
        /// <param name="referenceType">Type of the reference.</param>
        /// <param name="propertyInfo">The property info.</param>
        public ReferenceInfo(MetaDataStore store, string name, Type referenceType, PropertyInfo propertyInfo)
            : base(
                store,
                name,
                store.GetTableInfoFor(referenceType).PrimaryKey.DotNetType,
                store.GetTableInfoFor(referenceType).PrimaryKey.DbType,
                propertyInfo
            )
        {
            this.referenceType = referenceType;
        }

        /// <summary>
        /// Gets the type of the reference.
        /// </summary>
        /// <value>The type of the reference.</value>
        public Type ReferenceType { get { return this.referenceType; } }
    }
}
