using ClothShop.Data;
using ClothShop.Interface;
using ClothShop.Models;
using ClothShop.Services;
using ClothShop.ViewModels;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ClothShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _contextAccessor;
        private IUserRepository _userRepository;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            AppDbContext context,
            IHttpContextAccessor contextAccessor,
            IPhotoService photoService,
            IUserRepository userRepository)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
            _photoService = photoService;
            _userRepository = userRepository;
        }
        public IActionResult Login()
        {
            var response = new LoginVM();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View(login);

            var user = await _userManager.FindByEmailAsync(login.EmailAddress); // Находим юзверя по мылу
            if (user != null)
            {
                // Юзверь нашелся, смотрим на пароль
                var passwordCheck = await _userManager.CheckPasswordAsync(user, login.Password);
                if (passwordCheck)
                {// Пароль правильный, заходим в аккаунт
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Не правильный логин или пароль."; // Не желательно употреблять в большом количестве, лучше делать login.PasswordIsCorrect...
                return View(login);
            }
            // Юзверь не нашелся
            TempData["Error"] = "Не правильный логин или пароль.";
            return View(login);
        }

        private void MapUserEdit(AppUser user, EditAccountVM model, ImageUploadResult photoResult)
        {
            user.UserName = model.UserName;
            user.Cash = model.Cash;
            user.ProfileImageUrl = photoResult.Url.ToString();
            user.Region = model.Region;
            user.City = model.City;
            user.Street = model.Street;
            user.HouseNumber = model.HouseNumber;
        }

        public IActionResult Register()
        {
            var response = new RegisterVM();
            return View(response); // Посылаем в вид поля для заполнения регистрации
        }
        [HttpPost]

        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.EmailAddress); // Находим юзверя по мылу

            if (user != null) // Если уже такой есть, то ашипка...
            {
                TempData["Error"] = "Пользователь с такой почтой уже существует.";
                return View(model);
            }

            var newUser = new AppUser()
            {
                Email = model.EmailAddress,
                UserName = model.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password); // Создание юзверя с почтой и паролем

            if (newUserResponse.Succeeded) await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Profile()
        {

            var curUserId = _contextAccessor.HttpContext.User.GetUserId();
            var user = await _userRepository.GetByIdAsyncNoTracking(curUserId);
            
            return View(user);
        }

        public async Task<IActionResult> Edit()
        {
            var curUserId = _contextAccessor.HttpContext.User.GetUserId();
            var user = await _userRepository.GetByIdAsyncNoTracking(curUserId);

            if (user == null) return View("Error");
            var userVM = new EditAccountVM
            {
                //Id = user.Id,
                UserName = user.UserName,
                Url = user.ProfileImageUrl,
                Cash = user.Cash,
                AddressId = user.AddressId,
                Address = user.Address,
                Region = user.Region,
                City = user.City,
                Street = user.Street,
                HouseNumber = user.HouseNumber,
            };
            return View(userVM);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAccountVM accountVM)
        {
            if (!ModelState.IsValid) return RedirectToAction("Profile", "Account");
            var curUserId = _contextAccessor.HttpContext.User.GetUserId();
            var user = await _userRepository.GetByIdAsyncNoTracking(curUserId);
            if (user.ProfileImageUrl == null || user.ProfileImageUrl == "")
            {

                var photoResult = await _photoService.AddPhotoAsync(accountVM.Image);
                MapUserEdit(user, accountVM, photoResult);
                _userRepository.Update(user);

                return RedirectToAction("Profile");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception)
                {

                    ModelState.AddModelError("", "Fail");

                    return View(accountVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(accountVM.Image);
                MapUserEdit(user, accountVM, photoResult);
                _userRepository.Update(user);
                return RedirectToAction("Profile");

            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout() // Выход из системы.
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<ViewResult> InvokeAsync()
        {
            return View("AccountInfo", _context.Model);
        }

    }
}

