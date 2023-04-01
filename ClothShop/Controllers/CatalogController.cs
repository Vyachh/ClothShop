using ClothShop.Interface;
using ClothShop.Models.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace ClothShop.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IItemRepository _itemRepository;
        public CatalogController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _itemRepository.GetAllItems();
            return View(items);
        }

        public async Task<IActionResult> Item(int id)
        {
            //var items = 
            return View();
        }

    }
}
