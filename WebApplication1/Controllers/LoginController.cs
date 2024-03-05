using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Login(string ReturnUrl)
		{
			return View(new UserInfo() { ReturnUrl = ReturnUrl});
		}

		[HttpPost]
		public async Task<IActionResult> LoginAsync(UserInfo login)
		{
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.Name, login.Password, login.SaveUserInfo, false);
                if (result.Succeeded)
                {
                    return Redirect(login.ReturnUrl ?? "/Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильное имя или пароль");
                }
            }
            return View(login);
        }

        public IActionResult Register(string? ReturnUrl)
        {
            return View(new UserRegViewModel() { ReturnUrl = ReturnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(UserRegViewModel register)
        {
            if (register.Name == register.Password)
            {
                ModelState.AddModelError("", "Имя и пароль не должны совпадать");
            }
            if (ModelState.IsValid)
            {
                User user = new User { Email = register.Email, UserName = register.Name, PhoneNumber = register.Number, Age = register.Age };
                var result = await _usersManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    await _usersManager.AddToRoleAsync(user, Constants.UserRoleName);
                    return Redirect(register.ReturnUrl ?? "/Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(register);
        }

        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
