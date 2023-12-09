using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserRepository Users;

        public RegisterController(IUserRepository users)
        {
            Users = users;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserReg user)
        {
            if (user.Name == user.Password)
            {
                ModelState.AddModelError("", "Имя и пароль не должны совпадать");
            }
            var tryingToCheckExistingUser = Users.GetUsers().FirstOrDefault(use => use.Name == user.Name);

            if (tryingToCheckExistingUser != null)
            {
                ModelState.AddModelError("", "Пользователь с таким именем уже существует!!!");
            }

            if (ModelState.IsValid)
            {
                Users.Add(user);
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }
    }
}
