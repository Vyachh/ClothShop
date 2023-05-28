using ClothShop.Models.Catalog;

namespace ClothShop.ViewModels
{
    public class AccountVM
    {
        public string Id { get; set; }
        public decimal? Cash { get; set; }
        public IEnumerable<Item> Goods { get; set; }
    }
}
