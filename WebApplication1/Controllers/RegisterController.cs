using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser(UserReg user)
        {
            if (user.Name == user.Password)
            {
                ModelState.AddModelError("", "Имя и пароль не должны совпадать");
            }

            if (ModelState.IsValid)
            {
                return Content($"{user.Name} {user.Password} {user.PassCheck()}");
            }
            return Content(user.ToString());
        }
    }
}
