﻿using System;

namespace DDD.Data.MetaData
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class PrimaryKeyAttribute : Attribute
    {
        private readonly string columnName;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrimaryKeyAttribute"/> class.
        /// </summary>
        public PrimaryKeyAttribute()
        {
            this.columnName = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrimaryKeyAttribute"/> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        public PrimaryKeyAttribute(string columnName)
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
