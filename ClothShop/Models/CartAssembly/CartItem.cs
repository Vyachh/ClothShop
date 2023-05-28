using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ClothShop.Models.CartAssembly
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string UserId { get; set; }
        public string Image { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CartItemCount { get; set; }
    }
}
