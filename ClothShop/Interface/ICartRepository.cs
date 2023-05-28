using ClothShop.Models.CartAssembly;
using ClothShop.Models.Catalog;

namespace ClothShop.Interface
{
    public interface ICartRepository
    {
        Task<Cart> GetCart(string userId);
        void AddItem(Item item);
        void RemoveItem(Item item);
        void Clean();
    }
}
