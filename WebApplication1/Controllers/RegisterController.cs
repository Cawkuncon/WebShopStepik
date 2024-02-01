using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserRegDbRepository Users;

        public RegisterController(IUserRegDbRepository users)
        {
            Users = users;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegViewModel user)
        {
            if (user.Name == user.Password)
            {
                ModelState.AddModelError("", "Имя и пароль не должны совпадать");
            }
            var tryingToCheckExistingUser = Users.GetAll().FirstOrDefault(use => use.Name == user.Name);

            if (tryingToCheckExistingUser != null)
            {
                ModelState.AddModelError("", "Пользователь с таким именем уже существует!!!");
            }

            if (ModelState.IsValid)
            {
                var userToAdd = new UserReg();
                userToAdd.Name = user.Name;
                userToAdd.Password = user.Password;
                userToAdd.Age= user.Age;
                userToAdd.Email = user.Email;
                userToAdd.Number= user.Number;
                userToAdd.Password2= user.Password2;
                Users.Add(userToAdd);
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }
    }
}
