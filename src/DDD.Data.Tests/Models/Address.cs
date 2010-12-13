
using DDD.Data.MetaData;
namespace DDD.Data.Tests.Models
{
    [Table("Addresses")]
    public class Address
    {
        [PrimaryKey]
        public int? Id { get; set; }
    }
}
