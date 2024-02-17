using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;
using WebApplication1.Areas.Admin.Models;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserManager<User> UsersRepository;
        private RoleManager<IdentityRole> RolesRepository;
        public UserController(RoleManager<IdentityRole> rolesRepository, UserManager<User> usersRepository)
        {
            RolesRepository = rolesRepository;
            UsersRepository = usersRepository;
        }
        public IActionResult Index()
        {
            var user = UsersRepository.FindByNameAsync(User.Identity.Name);
            return View(user);
        }

        public IActionResult EditPassword(string Name)
        {
            ViewBag.Name = Name;
            return View();
        }

        [HttpPost]
        public IActionResult EditPassword(ChangePassword changePassword)
        {
            var user = UsersRepository.FindByNameAsync(changePassword.Name).Result;
            if (changePassword.Name == changePassword.Password)
            {
                ModelState.AddModelError("", "Пароли не должен совпадать с именем");
            }
            if (ModelState.IsValid)
            {
                var newPasswordHash = UsersRepository.PasswordHasher.HashPassword(user, changePassword.Password);
                user.PasswordHash = newPasswordHash;
                UsersRepository.UpdateAsync(user).Wait();
                return RedirectToAction("UserInfoCheck", "Home", new { Area = "Admin", changePassword.Name });
            }
            return View(changePassword);
        }

        public IActionResult EditUserInfo(string Name)
        {
            var user = UsersRepository.FindByNameAsync(Name).Result;
            var userViewModel = new UserRegViewModel();
            userViewModel.Name = user.UserName;
            userViewModel.Email = user.Email;
            userViewModel.Number = user.PhoneNumber;
            userViewModel.Age = user.Age;
            return View(userViewModel);
        }

        [HttpPost]
        public IActionResult EditUserInfo(UserRegViewModel user, string Name)
        {
            var userDB = UsersRepository.FindByNameAsync(Name).Result;
            userDB.Age = user.Age;
            userDB.PhoneNumber = user.Number;
            userDB.Email = user.Email;
            UsersRepository.UpdateAsync(userDB).Wait();
            return RedirectToAction("Index", "User");
        }
    }
}
