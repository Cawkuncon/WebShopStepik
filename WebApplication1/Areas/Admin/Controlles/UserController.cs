using Microsoft.AspNetCore.Mvc;
using WebApplication1.Areas.Admin.Models;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controlles
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserRepository UsersRepository;
        private readonly IRolesRepository rolesRepository;

        public UserController(IUserRepository usersRepository, IRolesRepository rolesRepository)
        {
            UsersRepository = usersRepository;
            this.rolesRepository = rolesRepository;
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

        public IActionResult UserRole(string Name)
        {
            var user = UsersRepository.GetUser(Name);
            ViewBag.Roles = rolesRepository.GetAll();
            return View(user);
        }

        [HttpPost]
        public IActionResult ChangeUserRole(string Name, string UserRole) 
        {
            var user = UsersRepository.GetUser(Name);
            var UsRole = rolesRepository.GetRole(UserRole);
            user.Role = UsRole;
            return RedirectToAction("UserInfoCheck", "Home", new { Area = "Admin", Name = Name });
        }

    }
}
