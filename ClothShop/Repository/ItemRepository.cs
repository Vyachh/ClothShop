using ClothShop.Data;
using ClothShop.Interface;
using ClothShop.Models.Catalog;
using Microsoft.EntityFrameworkCore;

namespace ClothShop.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _dbContext;
        public ItemRepository(AppDbContext context) { 
            _dbContext = context;
        }
        public bool Add(Item item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _dbContext.Items.ToListAsync();
        }

        public Task<Item> GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
