using ClothShop.Data;
using ClothShop.Interface;
using ClothShop.Models.CartAssembly;
using ClothShop.Models.Catalog;
using ClothShop.Repository;
using ClothShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClothShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        public CartController(IItemRepository itemRepository, IUserRepository userRepository, ICartRepository cartRepository)
        {
            _itemRepository = itemRepository;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userRepository.GetUser();
            var cart = await _cartRepository.GetCart(user.Id);
            var cartNCashVM = new CartNCashVM()
            {
                Cash = user.Cash,
                Cart = cart
            };
            return View(cartNCashVM);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            
            _cartRepository.AddItem(item);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            _cartRepository.RemoveItem(item);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> MakeDeal(decimal total)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userRepository.GetUser();
                user.Cash = user.Cash >= total ? user.Cash - total : user.Cash;
                _cartRepository.Clean();
                _userRepository.Update(user);
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> CalculateTotal(int count,int id)
        {
            var user = await _userRepository.GetUser();
            var cart = await _cartRepository.GetCart(user.Id);
            cart.CartItems.FirstOrDefault(x => x.Id == id).CartItemCount = count;
            _userRepository.Update(user);
            var result = new CartNCashVM()
            {
                Cash = user.Cash,
                Cart = cart
            };
            return View("Index",result);
        }
    }
}
