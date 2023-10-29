using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index(int a, int b)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class startController : Controller
    {
        public string hello()
        {
            string hello = string.Empty;
            DateTime dateTime = DateTime.Now;
            var time = dateTime.Hour;
            if (time >= 0 && time < 6)
            {
                return "Доброй ночи";
            }
            else if (time >= 6 && time < 12)
            {
                return "Доброе утро";
            }
            else if (time >= 12 && time < 18)
            {
                return "Добрый день";
            }
            else if (time >= 18 && time <= 23)
            {
                return "Добрый вечер";
            }
            return "Приветствую";
        }
    }
}