using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string RegisterUser(UserReg user)
        {
            return $"{user.Name} {user.Password} {user.PassCheck()}";
        }
    }
}
