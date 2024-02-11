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

        public IActionResult DeleteUser(Guid Id)
        {
            //var user = UsersRepository.Get(Id);
            //if (user != null)
            //{
            //    UsersRepository.Delete(Id);
            //}
            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        }

        public IActionResult EditUserInfo(Guid Id)
        {
            //var user = UsersRepository.Get(Id);
            var userViewModel = new UserRegViewModel();
            //userViewModel.Name = user.Name;
            //userViewModel.Email = user.Email;
            //userViewModel.Number = user.Number;
            //userViewModel.Age = user.Age;
            //userViewModel.UserId = user.Id;
            //userViewModel.Password = user.Password;
            //userViewModel.Password2 = user.Password2;
            return View(userViewModel);
        }

        [HttpPost]
        public IActionResult EditUserInfo(UserRegViewModel user, Guid Id)
        {
            var DictArgUser = new Dictionary<string, string>();
            //DictArgUser["Number"] = user.Number;
            //DictArgUser["Age"] = user.Age.ToString();
            //DictArgUser["Email"] = user.Email;
            //UsersRepository.UpdateUserInfo(Id, DictArgUser);
            return RedirectToAction("UserInfoCheck", "Home", new { Area = "Admin", Id });
        }

        public IActionResult EditPassword(string Name)
        {
            var user = UsersRepository.FindByNameAsync(Name).Result;
            var userViewModel = new UserRegViewModel();
            userViewModel.Name = user.UserName;
            userViewModel.Email = user.Email;
            userViewModel.Number = user.PhoneNumber;
            userViewModel.Age = user.Age;
            //userViewModel.UserId = user.Id;
            //userViewModel.Password = user.Password;
            //userViewModel.Password2 = user.Password2;
            return View(userViewModel);
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
