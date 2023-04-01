using System.ComponentModel.DataAnnotations;

namespace ClothShop.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
    }
}
