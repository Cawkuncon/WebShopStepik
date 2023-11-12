using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class BasketController : Controller
    {
		private readonly ProductRepository productRepository;
		private readonly Bask bask;

		public BasketController(ProductRepository productRepository, Bask bask)
        {
			this.productRepository = productRepository;
			this.bask = bask;
		}
        public IActionResult Adds(string productId)
        {
            var id = int.Parse(productId);
            var Prod = productRepository.GetAll().Where(x=>x.Id == id).First();
            bask.AddToBask(Prod);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var k = bask.GetBask().GroupBy(x=>x.Id);
            foreach (var item in k)
            {
                var id = item.Key;
                var name = item.First().Name;
                var count = item.Count();
                var cost = item.First().Cost;
                var descr = item.First().Description;
                var newProd = new Product(id, cost, name, descr, count);
                bask.AddToResultCart(newProd);
            }
            return View(bask.GetResultCart());
        }
    }
}
