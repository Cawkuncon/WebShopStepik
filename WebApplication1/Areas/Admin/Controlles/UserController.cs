using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controlles
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserRepository UsersRepository;

        public UserController(IUserRepository usersRepository)
        {
            UsersRepository = usersRepository;
        }

        public IActionResult DeleteUser(string Name)
        {
            var user = UsersRepository.GetUser(Name);
            UsersRepository.DeleteUser(user);
            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        }

        public IActionResult EditUserInfo(string Name)
        {
            var user = UsersRepository.GetUser(Name);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUserInfo(UserReg user, string Name)
        {
            var us = UsersRepository.GetUser(Name);
            us.Number = user.Number;
            us.Age = user.Age;
            us.Email = user.Email;
            return RedirectToAction("UserInfoCheck", "Home", new { Area = "Admin", Name = Name });
        }

        public IActionResult EditPassword(string name)
        {
            var user = UsersRepository.GetUser(name);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditPassword(string name, string password, string password2)
        {
            var user = UsersRepository.GetUser(name);
            user.Password = password;
            user.Password2 = password2;
            return RedirectToAction("UserInfoCheck", "Home", new { Name = user.Name });

        }

    }
}
