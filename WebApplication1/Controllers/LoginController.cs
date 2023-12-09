using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class LoginController : Controller
	{
        private readonly IUserRepository Users;

        public LoginController(IUserRepository users)
		{
            Users = users;
        }
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(UserInfo userInfo)
		{
            var ResUser = Users.GetUser(userInfo.Name);

            if (ResUser == null || ResUser.Password != userInfo.Password)
            {
                ModelState.AddModelError("", "Неверный пароль или неверное имя пользователя");
            }
            if (ModelState.IsValid)
			{
				return RedirectToAction("Index", "Home");
			}
			return View(userInfo);
		}
	}
}
