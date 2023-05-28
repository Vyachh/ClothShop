using ClothShop.Interface;
using ClothShop.Models.CartAssembly;
using ClothShop.Services;

namespace ClothShop.ViewModels
{
    public class CartNCashVM
    {

        public Cart Cart { get; set; }
        public decimal? Cash { get; set; }
        public decimal Total { get; set; }
    }
}
