using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DDD.Domain.Storage
{
    public static class ExtensionMethods
    {
        public static void CreateAndAddInputParameter(
            this IDbCommand command,
            string name,
            DbType dbType,
            object value)
        {
            var parameter = command.CreateParameter();

            parameter.Direction = ParameterDirection.Input;
            parameter.DbType = dbType;
            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;

            command.Parameters.Add(parameter);
        }

        public static void CreateAndAddOuputParameter(
            this IDbCommand command,
            string name,
            DbType dbType)
        {
            var parameter = command.CreateParameter();

            parameter.Direction = ParameterDirection.Output;
            parameter.DbType = dbType;
            parameter.ParameterName = name;

            command.Parameters.Add(parameter);
        }

        public static void CreateAndAddInputOuputParameter(
            this IDbCommand command,
            string name,
            DbType dbType,
            object value)
        {
            var parameter = command.CreateParameter();

            parameter.Direction = ParameterDirection.InputOutput;
            parameter.DbType = dbType;
            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;

            command.Parameters.Add(parameter);
        }
    }
}
