using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class BasketController : Controller
    {

        public IActionResult Adds(string productId)
        {
            var id = int.Parse(productId);
            var Prod = ProductRepository.GetAll().Where(x=>x.Id == id).First();
            Bask.prodCart.Add(Prod);
            var k = Bask.prodCart;

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var k = Bask.prodCart.GroupBy(x=>x.Id);
            var newList = new List<Product>();
            foreach (var item in k)
            {
                var id = item.Key;
                var name = item.First().Name;
                var count = item.Count();
                var cost = item.First().Cost;
                var descr = item.First().Description;
                var newProd = new Product(id, cost, name, descr, count);
                newList.Add(newProd);
            }
            return View(newList);
        }
    }
}
