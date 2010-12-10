using DDD.Data.MetaData;

namespace DDD.Data.Tests.Models
{
    [Table("Products")]
    public class Product
    {
        [PrimaryKey("Id")]
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
