using ClothShop.Models;
using ClothShop.Models.Catalog;

namespace ClothShop.Interface
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllItems();
        Task<Item> GetCategoryById(int id);
        //Task<Item> GetByIdAsyncNoTracking(string id);

        bool Add(Item item);
        bool Update(Item item);
        bool Delete(int id);
        bool Save();
    }
}
