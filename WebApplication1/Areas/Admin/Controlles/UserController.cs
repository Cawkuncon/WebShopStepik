using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using WebApplication1.Areas.Admin.Models;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controlles
{
    [Area("Admin")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UserController : Controller
    {
        private UserManager<User> UsersRepository;
        private RoleManager<IdentityRole> RolesRepository;
        public UserController(UserManager<User> users, RoleManager<IdentityRole> rolesRepository)
        {
            UsersRepository = users;
            RolesRepository = rolesRepository;
        }

        public IActionResult DeleteUser(string Name)
        {
            var user = UsersRepository.FindByNameAsync(Name).Result;
            UsersRepository.DeleteAsync(user).Wait();
            return RedirectToAction("Index", "Home", new { Area = "Admin" });
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
            return RedirectToAction("UserInfoCheck", "Home", new { Area = "Admin", Name });
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

        public IActionResult UserRole(Guid Id)
        {
            //var user = UsersRepository.Get(Id);
            //ViewBag.Roles = rolesRepository.GetAll();
            return View(null);
        }

        [HttpPost]
        public IActionResult ChangeUserRole(Guid UserId, Guid? UserRole) 
        {
            //Role role = null;
            //if (UserRole != null)
            //{
            //    role = rolesRepository.GetRole(UserRole);
            //}
            //UsersRepository.AddRole(UserId, role);
            return RedirectToAction("UserInfoCheck", "Home", new { Area = "Admin", Id = UserId });

        }

    }
}
