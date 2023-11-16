using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class BasketController : Controller
	{
		private readonly IProductRepository productRepository;
		private readonly IBaskRepository bask;

		public BasketController(IProductRepository productRepository, IBaskRepository bask)
		{
			this.productRepository = productRepository;
			this.bask = bask;
		}
		public IActionResult Adds(string productId)
		{
			var id = int.Parse(productId);
			var Prod = productRepository.GetAll().Where(x => x.Id == id).First();
			bask.AddToCart(Prod);
			return RedirectToAction("Index");
		}
		public IActionResult Index()
		{
			var k = bask.GetCart().GroupBy(x => x.Id);
			bask.ClearResultProducts();
			foreach (var item in k)
			{
				var id = item.Key;
				var name = item.First().Name;
				var count = item.Count();
				var cost = item.First().Cost;
				var descr = item.First().Description;
				var newProd = new Product(id, cost, name, descr, count);
				bask.AddToResultProducts(newProd);
			}
			var relustProductsOrdered = bask.GetResultProducts();
			relustProductsOrdered = relustProductsOrdered.OrderBy(x => x.Name).ToList();
			return View(relustProductsOrdered);
		}

		public IActionResult Clear()
		{
			bask.ClearCart();
			return RedirectToAction("Index");
		}

		public IActionResult Plus(int Id)
		{
			var prod = bask.GetCart().Where(x => x.Id == Id).First();
			bask.AddToCart(prod);
			return RedirectToAction("Index");
		}

		public IActionResult Minus(int Id)
		{
			bask.RemoveFromCart(Id);
			return RedirectToAction("Index");
		}

		public IActionResult RegisterOrder()
		{
			return View(bask.GetResultProducts());
		}

		[HttpPost]
		public IActionResult Success(Order order)
		{
			bask.ClearCart();
			bask.ClearResultProducts();
			return View();
		}
	}
}
