using DDD.Data.MetaData;

namespace DDD.Data.Tests.Models
{
    [Table("Customers")]
    public class Customer
    {
        [PrimaryKey("Id")]
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
