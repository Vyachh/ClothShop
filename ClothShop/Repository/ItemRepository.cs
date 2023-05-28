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
            _dbContext.Items.Add(item);
            return Save();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Item>> GetProducts()
        {
            return await _dbContext.Items.Include(item => item.SizeFeatures).ToListAsync();
        }

        public Task<Item> GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            return await _dbContext.Items.FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;

        }

        public bool Update(Item item)
        {
            _dbContext.Update(item);
            return Save();
        }

        public async Task<List<Item>> GetByConcurrency(string text)
        {
            List<Item> aa = await _dbContext.Items.ToListAsync(); // Looks pretty unoptimized :\
            var items = aa.FindAll(x => x.Name.Contains(text));
            return items;
        }
    }
}
