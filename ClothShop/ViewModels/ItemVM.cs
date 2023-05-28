using ClothShop.Models.Catalog;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClothShop.ViewModels
{
    public class ItemVM : Item
    {
        public IFormFile Image { get; set; }
        public decimal? Cash { get; set; }
        public int Quantity { get; set; }

    }
}
