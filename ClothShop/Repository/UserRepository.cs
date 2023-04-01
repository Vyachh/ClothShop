using ClothShop.Data;
using ClothShop.Interface;
using ClothShop.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothShop.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext context)
        {
            _dbContext = context;
        }
        public bool Add(AppUser user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AppUser>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetByIdAsyncNoTracking(string id)
        {
            return await _dbContext.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public Task<AppUser> GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public bool Update(AppUser user)
        {
            _dbContext.Users.Update(user);
            return Save();
        }
        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
