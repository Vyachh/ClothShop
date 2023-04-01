using ClothShop.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothShop.ViewModels
{
    public class EditAccountVM
    {
        //public string Id { get; set; }
        public string? UserName { get; set; }
        //public string? Password { get; set; }
        //public string? Email { get; set; }
        public IFormFile Image { get; set; }
        public string? Url { get; set; }
        public int? Cash { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int HouseNumber { get; set; }
    }
}
