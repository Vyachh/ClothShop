using ClothShop.Data;
using ClothShop.Enum;
using ClothShop.Interface;
using ClothShop.Models;
using ClothShop.Models.Catalog;
using ClothShop.Services;
using ClothShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClothShop.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IPhotoService _photoService;
        public CatalogController(IUserRepository userRepository, IItemRepository itemRepository, IPhotoService photoService)
        {
            _userRepository = userRepository;
            _itemRepository = itemRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _itemRepository.GetProducts();

            var itemListVM = new ItemListVM { Goods = items };

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userRepository.GetUser();
                itemListVM.Cash = user.Cash;
            }

            return View(itemListVM);
        }

        public async Task<IActionResult> ItemInfo(int id)
        {

            var items = await _itemRepository.GetProducts();
            var item = items.FirstOrDefault(x => x.Id == id);
            var itemInfoVM = new ItemInfoVM()
            {
                UserId = item.UserId,
                Name = item.Name,
                Image = item.Image,
                Price = item.Price,
                Description = item.Description,
                Quantity = item.Quantity,
                SizeFeatures = item.SizeFeatures,
                Category = item.Category,
                Cash = 0
            };
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userRepository.GetUser();
                itemInfoVM.Cash = user.Cash;
            }
            return View(itemInfoVM);
        }

        public async Task<IActionResult> Search(string text)
        {
            var items = await _itemRepository.GetByConcurrency(text);
            return View(items);
        }

        public async Task<IActionResult> Product(int? categoryId)
        {
            var products = _itemRepository.GetProducts();
            IEnumerable<Item> items = await products;

            Category curCategory = (Category)System.Enum.GetValues(typeof(Category)).GetValue((int)categoryId);

            if (categoryId.HasValue)
            {
                items = items.Where(p => p.Category == curCategory);
            }

            var itemListVM = new ItemListVM { Goods = items };
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userRepository.GetUser();
                itemListVM.Cash = user.Cash;
            }

            return View(itemListVM);
        }

        public async Task<IActionResult> CreateItem()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userRepository.GetUser();

                var item = new ItemVM()
                {
                    UserId = user.Id,
                    Cash = user.Cash
                };

                return View(item);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemVM itemVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(itemVM.Image);
                var item = new Item
                {
                    Name = itemVM.Name,
                    Image = result.Url.ToString(),
                    Price = itemVM.Price,
                    Description = itemVM.Description,
                    SizeFeatures = itemVM.SizeFeatures,
                    Category = itemVM.Category,
                    UserId = itemVM.UserId,
                    Quantity = itemVM.Quantity,
                };
                _itemRepository.Add(item);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(itemVM);

        }
    }
}
