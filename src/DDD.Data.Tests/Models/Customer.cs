using DDD.Data.MetaData;

namespace DDD.Data.Tests.Models
{
    [Table("Customers")]
    public class Customer
    {
        [PrimaryKey]
        public int? Id { get; set; }
        [Column]
        public string FirstName { get; set; }
        [Column]
        public string LastName { get; set; }
        [Reference]
        public Address Address { get; set; }
    }
}
