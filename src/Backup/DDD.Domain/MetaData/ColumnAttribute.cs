using System;

namespace DDD.Domain.MetaData
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class ColumnAttribute : Attribute
    {
        private readonly string columnName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnAttribute"/> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        public ColumnAttribute(string columnName)
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
