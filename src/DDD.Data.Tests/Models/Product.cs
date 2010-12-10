using DDD.Data.MetaData;

namespace DDD.Data.Tests.Models
{
    [Table("Products")]
    public class Product
    {
        [PrimaryKey("Id")]
        public int? Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Description")]
        public string Description { get; set; }
    }
}
