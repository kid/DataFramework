using System;
using System.Collections.Generic;

namespace DDD.Data.MetaData
{
    public class TableInfo : MetaData
    {
        private readonly string name;
        private readonly Type entityType;
        private ColumnInfo primaryKey;
        private readonly Dictionary<string, ColumnInfo> columns = new Dictionary<string, ColumnInfo>();
        private readonly Dictionary<string, ReferenceInfo> references = new Dictionary<string, ReferenceInfo>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TableInfo"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="name">The name.</param>
        /// <param name="entityType">Type of the entity.</param>
        public TableInfo(MetaDataStore store, string name, Type entityType)
            : base(store)
        {
            this.name = name;
            this.entityType = entityType;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get { return this.name; } }

        /// <summary>
        /// Gets the type of the entity.
        /// </summary>
        /// <value>The type of the entity.</value>
        public Type EntityType { get { return this.entityType; } }

        /// <summary>
        /// Gets the primary key.
        /// </summary>
        /// <value>The primary key.</value>
        public ColumnInfo PrimaryKey
        {
            get { return this.primaryKey; }
            internal set { this.primaryKey = value; }
        }

        /// <summary>
        /// Gets the columns.
        /// </summary>
        /// <value>The columns.</value>
        public IEnumerable<ColumnInfo> Columns { get { return this.columns.Values; } }

        /// <summary>
        /// Gets the references.
        /// </summary>
        /// <value>The references.</value>
        public IEnumerable<ReferenceInfo> References { get { return this.references.Values; } }

        /// <summary>
        /// Adds the column.
        /// </summary>
        /// <param name="column">The column.</param>
        public void AddColumn(ColumnInfo column)
        {
            if (this.columns.ContainsKey(column.Name))
            {
                throw new InvalidOperationException(
                    string.Format("An item with key {0} has already been added", column.Name)
                );
            }
            this.columns.Add(column.Name, column);
        }

        /// <summary>
        /// Adds the reference.
        /// </summary>
        /// <param name="reference">The reference.</param>
        public void AddReference(ReferenceInfo reference)
        {
            if (this.references.ContainsKey(reference.Name))
            {
                throw new InvalidOperationException(
                    string.Format("An item with key {0} has already been added", reference.Name)
                );
            }
            this.references.Add(reference.Name, reference);
        }

        /// <summary>
        /// Gets the column.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        public ColumnInfo GetColumn(string columnName)
        {
            ColumnInfo column;
            if (!this.columns.TryGetValue(columnName, out column))
            {
                throw new InvalidOperationException(
                    string.Format("The table '{0}' does not have a '{1}' column", this.Name, columnName)
                );
            }
            return column;
        }

        public string GetGetByIdStoredProcName()
        {
            return string.Format("Get{0}", this.Name);
        }

        public string GetDeleteStoredProcName()
        {
            return string.Format("Delete{0}", this.Name);
        }
    }
}
