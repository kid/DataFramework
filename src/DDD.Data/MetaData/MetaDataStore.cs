using System;
using System.Collections.Generic;
using System.Reflection;

namespace DDD.Data.MetaData
{
    public class MetaDataStore
    {
        private readonly Dictionary<Type, TableInfo> typeToTableInfo = new Dictionary<Type, TableInfo>();

        /// <summary>
        /// Gets the table info for the specified type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        public TableInfo GetTableInfoFor<TEntity>()
        {
            return GetTableInfoFor(typeof(TEntity));
        }

        /// <summary>
        /// Gets the table info for the specified type.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns></returns>
        public TableInfo GetTableInfoFor(Type entityType)
        {
            TableInfo tableInfo;
            this.typeToTableInfo.TryGetValue(entityType, out tableInfo);
            return tableInfo;
        }

        /// <summary>
        /// Builds the meta data for the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public void BuildMetaDataFor(Assembly assembly)
        {
            BuildMapOfEntityTypesWithTheirTableInfo(assembly);

            foreach (var pair in typeToTableInfo)
            {
                LoopThroughPropertiesWith<PrimaryKeyAttribute>(pair.Key, pair.Value, this.SetPrimaryKeyInfo);
            }
            foreach (var pair in typeToTableInfo)
            {
                LoopThroughPropertiesWith<ReferenceAttribute>(pair.Key, pair.Value, this.AddReferenceInfo);
                LoopThroughPropertiesWith<ColumnAttribute>(pair.Key, pair.Value, this.AddColumnInfo);
            }
        }

        private void BuildMapOfEntityTypesWithTheirTableInfo(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                var typeAttributes = Attribute.GetCustomAttributes(type, typeof(TableAttribute));
                if (typeAttributes.Length > 0)
                {
                    var tableAttribute = (TableAttribute)typeAttributes[0];
                    var tableInfo = new TableInfo(this, tableAttribute.TableName, type);
                    this.typeToTableInfo.Add(type, tableInfo);
                }
            }
        }

        private void LoopThroughPropertiesWith<TAttribute>(
            Type entityType,
            TableInfo tableInfo,
            Action<TableInfo, PropertyInfo, TAttribute> andExecuteFollowingCode)
            where TAttribute : Attribute
        {
            foreach (var propertyInfo in entityType.GetProperties())
            {
                var attribbute = GetAttribute<TAttribute>(propertyInfo);
                if (attribbute != null)
                {
                    andExecuteFollowingCode(tableInfo, propertyInfo, attribbute);
                }
            }
        }

        private void SetPrimaryKeyInfo(
            TableInfo tableInfo,
            PropertyInfo propertyInfo,
            PrimaryKeyAttribute primaryKeyAttribute)
        {
            tableInfo.PrimaryKey = new ColumnInfo(
                this,
                primaryKeyAttribute.ColumnName ?? propertyInfo.Name,
                propertyInfo.PropertyType,
                propertyInfo
            );
        }

        private void AddColumnInfo(
            TableInfo tableInfo,
            PropertyInfo propertyInfo,
            ColumnAttribute columnAttribute)
        {
            tableInfo.AddColumn(
                new ColumnInfo(
                    this,
                    columnAttribute.ColumnName ?? propertyInfo.Name,
                    propertyInfo.PropertyType,
                    propertyInfo
                )
            );
        }

        private void AddReferenceInfo(
            TableInfo tableInfo,
            PropertyInfo propertyInfo,
            ReferenceAttribute referenceAttribute)
        {
            var columnName = referenceAttribute.ColumnName;
            if (columnName == null)
            {
                columnName = string.Concat(
                    propertyInfo.Name,
                    this.GetTableInfoFor(propertyInfo.PropertyType).PrimaryKey.Name
                );
            }
            tableInfo.AddReference(
                new ReferenceInfo(
                    this,
                    columnName,
                    propertyInfo.PropertyType,
                    propertyInfo
                )
            );
        }

        private TAttribute GetAttribute<TAttribute>(PropertyInfo propertyInfo)
            where TAttribute : Attribute
        {
            var attributes = Attribute.GetCustomAttributes(propertyInfo, typeof(TAttribute));
            return attributes.Length == 0 ? null : (TAttribute)attributes[0];
        }
    }
}
