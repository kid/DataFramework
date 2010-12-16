using System;
using DDD.Data.Storage;

namespace DDD.Data.MetaData
{
    public class ConventionBasedStoredProcNamingStrategy : IStoredProcNamingStrategy
    {
        public string GetStoredProcNameFor<TAction>()
        {
            var actionType = typeof(TAction);
            if (actionType.GetGenericTypeDefinition() == typeof(InsertAction<>))
            {
                return string.Format("{0}_Insert", actionType.GetGenericArguments()[0].Name);
            }
            if (actionType.GetGenericTypeDefinition() == typeof(UpdateAction<>))
            {
                return string.Format("{0}_Update", actionType.GetGenericArguments()[0].Name);
            }
            if (actionType.GetGenericTypeDefinition() == typeof(DeleteAction<>))
            {
                return string.Format("{0}_Delete", actionType.GetGenericArguments()[0].Name);
            }
            if (actionType.GetGenericTypeDefinition() == typeof(GetByIdAction<>))
            {
                return string.Format("{0}_GetById", actionType.GetGenericArguments()[0].Name);
            }
            throw new InvalidOperationException(string.Format(
                "Can't determine the stored procedure's name for {0}.",
                actionType.Name
            ));
        }
    }
}
