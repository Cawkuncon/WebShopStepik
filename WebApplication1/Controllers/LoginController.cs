using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public string TryToLogin(IUserInfo userInfo)
		{
			return ($"{userInfo.Name} {userInfo.Password} ");
		}
	}
}
