using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        readonly ProductRepository productsRepository;
        public HomeController()
        {
            productsRepository = new ProductRepository();
        }
        public IActionResult Index()
        {
            var products = productsRepository.GetAll();

            return View(products);
        }
    }
   
}