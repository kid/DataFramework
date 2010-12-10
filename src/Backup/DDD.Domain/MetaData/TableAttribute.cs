using System;

namespace DDD.Domain.MetaData
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class TableAttribute : Attribute
    {
        private readonly string tableName;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableAttribute"/> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        public TableAttribute(string tableName)
        {
            this.tableName = tableName;
        }

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <value>The name of the table.</value>
        public string TableName { get { return this.tableName; } }
    }
}
