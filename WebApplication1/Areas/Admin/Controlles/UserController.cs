using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;
using WebApplication1.Areas.Admin.Models;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controlles
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserRegDbRepository UsersRepository;
        private readonly IRolesRepository rolesRepository;

        public UserController(IUserRegDbRepository usersRepository, IRolesRepository rolesRepository)
        {
            UsersRepository = usersRepository;
            this.rolesRepository = rolesRepository;
        }

        public IActionResult DeleteUser(Guid Id)
        {
            var user = UsersRepository.Get(Id);
            if (user != null)
            {
                UsersRepository.Delete(Id);
            }
            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        }

        public IActionResult EditUserInfo(Guid Id)
        {
            var user = UsersRepository.Get(Id);
            var userViewModel = new UserRegViewModel();
            userViewModel.Name = user.Name;
            userViewModel.Email = user.Email;
            userViewModel.Number = user.Number;
            userViewModel.Age = user.Age;
            userViewModel.UserId = user.Id;
            userViewModel.Password = user.Password;
            userViewModel.Password2 = user.Password2;
            return View(userViewModel);
        }

        [HttpPost]
        public IActionResult EditUserInfo(UserRegViewModel user, Guid Id)
        {
            var DictArgUser = new Dictionary<string, string>();
            DictArgUser["Number"] = user.Number;
            DictArgUser["Age"] = user.Age.ToString();
            DictArgUser["Email"] = user.Email;
            UsersRepository.UpdateUserInfo(Id, DictArgUser);
            return RedirectToAction("UserInfoCheck", "Home", new { Area = "Admin", Id });
        }

        public IActionResult EditPassword(Guid Id)
        {
            var user = UsersRepository.Get(Id);
            var userViewModel = new UserRegViewModel();
            userViewModel.Name = user.Name;
            userViewModel.Email = user.Email;
            userViewModel.Number = user.Number;
            userViewModel.Age = user.Age;
            userViewModel.UserId = user.Id;
            userViewModel.Password = user.Password;
            userViewModel.Password2 = user.Password2;
            return View(userViewModel);
        }

        [HttpPost]
        public IActionResult EditPassword(Guid Id, string password, string password2)
        {
            var DictArgUser = new Dictionary<string,string>();
            DictArgUser["Password"] = password;
            DictArgUser["Password2"] = password2;
            UsersRepository.UpdateUserInfo(Id, DictArgUser);
            return RedirectToAction("UserInfoCheck", "Home", new { Area = "Admin", Id });

        }

        public IActionResult UserRole(Guid Id)
        {
            var user = UsersRepository.Get(Id);
            ViewBag.Roles = rolesRepository.GetAll();
            return View(user);
        }

        [HttpPost]
        public IActionResult ChangeUserRole(Guid UserId, Guid UserRole) 
        {
            var role = rolesRepository.GetRole(UserRole);
            UsersRepository.AddRole(UserId, role);
            return RedirectToAction("UserInfoCheck", "Home", new { Area = "Admin", Id = UserId });

        }

    }
}
