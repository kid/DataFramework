using DDD.Data.MetaData;

namespace DDD.Data.Tests.Models
{
    [Table("Addresses")]
    public class Address
    {
        [PrimaryKey]
        public int? Id { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
