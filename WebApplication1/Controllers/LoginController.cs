using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using Serilog;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class LoginController : Controller
	{
        private readonly UserManager<User> _usersManager;
        private readonly SignInManager<User> _signInManager;
        public LoginController(UserManager<User> usersManager, SignInManager<User> signInManager)
        {
            _usersManager = usersManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(UserInfo login)
		{
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(login.Name, login.Password, login.SaveUserInfo, false).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильное имя или пароль");
                }
            }
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegViewModel register)
        {
            if (register.Name == register.Password)
            {
                ModelState.AddModelError("", "Имя и пароль не должны совпадать");
            }
            if (ModelState.IsValid)
            {
                User user = new User { Email = register.Email, UserName = register.Name, PhoneNumber = register.Number };
                var result = _usersManager.CreateAsync(user, register.Password).Result;
                if (result.Succeeded)
                {
                    _signInManager.SignInAsync(user, false).Wait();
                    _usersManager.AddToRoleAsync(user, Constants.UserRoleName).Wait();
                    return Redirect(register.ReturnUrl ?? / "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                return View(register);
            }
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
