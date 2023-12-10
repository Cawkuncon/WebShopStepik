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
    }
}
