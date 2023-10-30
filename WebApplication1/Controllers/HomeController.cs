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

    public class CalculatorController : Controller
    {
        public string index(double a, double b, string operation)
        {
            operation = operation is null ? "+" : operation;
            var chars = new string[3] { "-", "+", "*"};
            if (!chars.Contains(operation))
            {
                return "Неправильная операция";
            }
            else
            {
                switch (operation)
                {
                    case "+":
                        return $"{a} + {b} = {a + b}";
                    case "-":
                        return $"{a} - {b} = {a - b}";
                    case "*":
                        return $"{a} * {b} = {a * b}";
                    default:
                        break;
                }
            }
            return string.Empty;
        }
    }
}