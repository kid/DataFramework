
namespace DDD.Data.MetaData
{
    public interface IStoredProcNamingStrategy
    {
        string GetStoredProcNameFor<TAction>();
    }
}
