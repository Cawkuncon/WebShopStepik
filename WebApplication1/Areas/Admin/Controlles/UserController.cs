using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controlles
{
    public class UserController : Controller
    {
        private readonly IUserRepository UsersRepository;

        public UserController(IUserRepository usersRepository)
        {
            UsersRepository = usersRepository;
        }

        public IActionResult Index(UserReg user)
        {
            UsersRepository.DeleteUser(user);
            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        }
    }
}
