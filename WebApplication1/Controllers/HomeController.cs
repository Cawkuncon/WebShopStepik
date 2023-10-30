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

        public string Index()
        {
            var result = string.Empty;
            var listProducts = new List<Product>()
            {
                new Product(1, 11, "Name1", "Descr1"),
                new Product(2, 22, "Name2", "Descr2"),
                new Product(3, 33, "Name3", "Descr3"),
            };
            
            foreach (var product in listProducts)
            {
                result += product;
            }
            return result;
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
}