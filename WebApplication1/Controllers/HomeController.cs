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

        public IActionResult AddToComparsion(string productId)
        {
            var id = int.Parse(productId);
            var prodToCompare = productRepository.GetAll().Where(x => x.Id == id).First();
            prodToCompare.Comparsion = !prodToCompare.Comparsion;
            return RedirectToAction("Index");
        }

        public IActionResult Compare()
        {
            var products = productRepository.GetAll().Where(x => x.Comparsion).OrderBy(x => x.Name).ToList();
            return View(products);
        }

        public IActionResult DeleteFromComparsion(string productId)
        {
            var id = int.Parse(productId);
            var prodToCompare = productRepository.GetAll().Where(x => x.Id == id).First();
            prodToCompare.Comparsion = !prodToCompare.Comparsion;
            return RedirectToAction("Compare");
        }

    }

}