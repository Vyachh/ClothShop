using ClothShop.Models;
using ClothShop.Models.Catalog;

namespace ClothShop.Interface
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetProducts();
        Task<Item> GetCategoryById(int id);
        //Task<Item> GetByIdAsyncNoTracking(string id);
        Task<Item> GetByIdAsync(int id);
        Task<List<Item>> GetByConcurrency(string text);
        bool Add(Item item);
        bool Update(Item item);
        bool Delete(int id);
        bool Save();
    }
}
