using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
		private readonly IProductRepository productRepository;

		public HomeController(IProductRepository productRepository)
        {
			this.productRepository = productRepository;
		}

        public IActionResult Index()
        {
            var products = productRepository.GetAll();

            return View(products);
        }
    }
   
}