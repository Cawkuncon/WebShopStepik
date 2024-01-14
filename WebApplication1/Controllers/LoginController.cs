using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class LoginController : Controller
	{
        private readonly IUserRegDbRepository Users;

        public LoginController(IUserRegDbRepository users)
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
            var DBUsers = Users.GetAll();
			var ResUser = DBUsers.FirstOrDefault(x => x.Name == userInfo.Name);

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
