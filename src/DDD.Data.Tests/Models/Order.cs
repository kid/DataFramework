using DDD.Data.MetaData;

namespace DDD.Data.Tests.Models
{
    [Table("Orders")]
    public class Order
    {
        [PrimaryKey("Id")]
        public int? Id { get; set; }

        [Reference("CustomerId")]
        public Customer Customer { get; set; }
    }
}
