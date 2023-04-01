using ClothShop.Data;
using ClothShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace ClothShop.ViewComponents
{
        [ViewComponent(Name = "Info")]
        public class AccountViewComponent : ViewComponent
        {
            private readonly AppDbContext _context;
            public AccountViewComponent(AppDbContext applicationDbContext)
            {
                _context = applicationDbContext;
            }

            public async Task<IViewComponentResult> InvokeAsync()
            {
            
                return View("AccountInfo", _context.Model);
            }
        }
    
}
