using ClothShop.Models.Catalog;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClothShop.ViewModels
{
    public class ItemListVM : Item
    {
        public decimal? Cash { get; set; }
        public IEnumerable<Item> Goods { get; set; }
    }
}
