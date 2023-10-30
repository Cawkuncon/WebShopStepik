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

    public class CalcController : Controller
    {
        public string Index(double a, double b, string c)
        {
            var chars = new string[4] { "+", "-", "/", "*" };
            if (c is not null && !chars.Contains(c))
            {
                return "Неверная операция";
            }
            switch (c)
            {
                case "+":
                    return $"{a} + {b} = {a + b}";
                case "-":
                    return $"{a} - {b} = {a - b}";
                case "*":
                    return $"{a} * {b} = {a * b}";
                case "/":
                    return $"{a} / {b} = {a / b}";
                case null:
                    goto case "+";
                default:
                    break;
            }
            return string.Empty;
        }
    }
}