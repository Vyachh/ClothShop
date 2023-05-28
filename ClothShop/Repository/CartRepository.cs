using ClothShop.Data;
using ClothShop.Interface;
using ClothShop.Models.CartAssembly;
using ClothShop.Models.Catalog;

namespace ClothShop.Repository
{
    public class CartRepository : ICartRepository
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly AppDbContext _dbContext;
        public CartRepository(IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }
        public void AddItem(Item item)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var cartItem = _dbContext.CartItems.FirstOrDefault(ci => ci.UserId == userId && ci.ProductId == item.Id.ToString());

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = item.Id.ToString(),
                    ProductName = item.Name,
                    Image = item.Image,
                    Description = item.Description,
                    Price = Convert.ToInt32(item.Price),
                    Quantity = item.Quantity,
                    CartItemCount = 1
                };
                _dbContext.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.CartItemCount++ ;
            }
            _dbContext.SaveChanges();
        }

        public void Clean()
        {
            foreach (var item in _dbContext.CartItems)
            {
                _dbContext.CartItems.Remove(item);
            }
            _dbContext.SaveChanges();

        }

        public async Task<Cart> GetCart(string userId)
        {
            var cartItems = _dbContext.CartItems.Where(ci => ci.UserId == userId).ToList();

            return new Cart
            {
                UserId = userId,
                CartItems = cartItems
            };
        }

        public void RemoveItem(Item item)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var cartItem = _dbContext.CartItems.FirstOrDefault(ci => ci.UserId == userId && ci.ProductId == item.Id.ToString());
            if (cartItem != null)
            {
                _dbContext.CartItems.Remove(cartItem);
                _dbContext.SaveChanges();
            }
        }
    }
}

