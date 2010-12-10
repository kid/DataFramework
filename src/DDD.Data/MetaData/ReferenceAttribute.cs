using System;

namespace DDD.Data.MetaData
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class ReferenceAttribute : Attribute
    {
        private readonly string columnName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceAttribute"/> class.
        /// </summary>
        public ReferenceAttribute()
        {
            this.columnName = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceAttribute"/> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        public ReferenceAttribute(string columnName)
        {
            this.columnName = columnName;
        }

        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        /// <value>The name of the column.</value>
        public string ColumnName { get { return this.columnName; } }
    }
}
