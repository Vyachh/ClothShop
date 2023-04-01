using ClothShop.Models;
using ClothShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClothShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            var curUser = await _userManager.GetUserAsync(User);
            if (curUser != null)
            {
                var model = new AccountVM
                {
                    Id = curUser.Id,
                    Cash = curUser.Cash
                };
                return View(model);
            }
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var curUser = await _userManager.GetUserAsync(User);
            if (curUser != null)
            {
                var model = new AccountVM
                {
                    Id = curUser.Id,
                    Cash = curUser.Cash
                };
                return View(model);
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}