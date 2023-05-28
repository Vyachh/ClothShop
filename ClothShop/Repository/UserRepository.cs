using ClothShop.Data;
using ClothShop.Interface;
using ClothShop.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothShop.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserRepository(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _dbContext = context;
            _contextAccessor = contextAccessor;
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

        public async Task<AppUser> GetUserById(string id)
        {
            return await _dbContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<AppUser> GetUser()
        {
            var userId = _contextAccessor.HttpContext.User.GetUserId();
            var user = await GetByIdAsyncNoTracking(userId);
            return user;
        }

        public bool Update(AppUser user)
        {
            _dbContext.Users.Update(user);
            return Save();
        }
        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0;
        }
    }
}
