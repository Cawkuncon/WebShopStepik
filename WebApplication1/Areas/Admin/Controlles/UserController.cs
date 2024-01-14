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
            return View(user);
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
            return View(user);
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
        public void ChangeUserRole(string Name, string UserRole) 
        {
            //var user = UsersRepository.GetUser(Name);
            //var UsRole = rolesRepository.GetRole(UserRole);
            //user.Role = UsRole;
            //return RedirectToAction("UserInfoCheck", "Home", new { Area = "Admin", Name = Name });
            
        }

    }
}
