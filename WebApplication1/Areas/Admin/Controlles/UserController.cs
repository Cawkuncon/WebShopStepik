using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private IImageDbRepository imageDbRepository;
        private IWebHostEnvironment webHostEnvironment;
        public UserController(UserManager<User> users, RoleManager<IdentityRole> rolesRepository, IImageDbRepository imageDbRepository, IWebHostEnvironment webHostEnvironment)
        {
            UsersRepository = users;
            RolesRepository = rolesRepository;
            this.imageDbRepository = imageDbRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> DeleteUserAsync(string Name)
        {
            var user = await UsersRepository.FindByNameAsync(Name);
            var image = await imageDbRepository.GetUserImageAsync(user);
            string path = Path.Combine(webHostEnvironment.WebRootPath + image.Path);
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            await imageDbRepository.DeleteImageAsync(user);
            await UsersRepository.DeleteAsync(user);
            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        }

        public async Task<IActionResult> EditUserInfoAsync(string Name)
        {
            var user = await UsersRepository.FindByNameAsync(Name);
            var userViewModel = new UserRegViewModel();
            userViewModel.Name = user.UserName;
            userViewModel.Email = user.Email;
            userViewModel.Number = user.PhoneNumber;
            userViewModel.Age = user.Age;
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserInfoAsync(UserRegViewModel user, string Name)
        {
            var userDB = await UsersRepository.FindByNameAsync(Name);
            userDB.Age = user.Age;
            userDB.PhoneNumber = user.Number;
            userDB.Email = user.Email;
            await UsersRepository.UpdateAsync(userDB);
            return RedirectToAction("UserInfoCheck", "Home", new { Area = "Admin", Name });
        }

        public IActionResult EditPassword(string Name)
        {
            ViewBag.Name = Name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditPasswordAsync(ChangePassword changePassword)
        {
            var user = await UsersRepository.FindByNameAsync(changePassword.Name);
            if (changePassword.Name == changePassword.Password)
            {
                ModelState.AddModelError("", "Пароли не должен совпадать с именем");
            }
            if (ModelState.IsValid)
            {
                var newPasswordHash = UsersRepository.PasswordHasher.HashPassword(user, changePassword.Password);
                user.PasswordHash = newPasswordHash;
                await UsersRepository.UpdateAsync(user);
                return RedirectToAction("UserInfoCheck", "Home", new { Area = "Admin", changePassword.Name });
            }
            return View(changePassword);

        }

        public async Task<IActionResult> UserRoleAsync(string Name)
        {
            var user = await UsersRepository.FindByNameAsync(Name);
            var userRoles = await UsersRepository.GetRolesAsync(user);
            ViewBag.Roles = RolesRepository.Roles;
            ViewBag.RolesCount = RolesRepository.Roles.Count();
            ViewBag.UserRole = userRoles.First();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserRoleAsync(string UserName, string UserRole) 
        {
            var user = await UsersRepository.FindByNameAsync(UserName);
            var role = await RolesRepository.FindByNameAsync(UserRole);
            if (role != null && user != null) 
            {
                var userRole = (await UsersRepository.GetRolesAsync(user)).First();
                await UsersRepository.RemoveFromRoleAsync(user, userRole);
                await UsersRepository.UpdateAsync(user);
                await UsersRepository.AddToRoleAsync(user, role.Name);
                await UsersRepository.UpdateAsync(user);
            }

            return RedirectToAction("UserInfoCheck", "Home", new { Area = "Admin", Name = UserName});
        }
    }
}
