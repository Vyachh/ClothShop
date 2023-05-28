using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothShop.Models
{
    public class AppUser : IdentityUser
    {
        public string? ProfileImageUrl { get; set; }
        public decimal? Cash { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        public string? Region { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int HouseNumber { get; set; }
    }
}
