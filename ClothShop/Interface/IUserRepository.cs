using ClothShop.Models;

namespace ClothShop.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdAsyncNoTracking(string id);
        Task<AppUser> GetUser();

        bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(string id);
        bool Save();
    }
}
