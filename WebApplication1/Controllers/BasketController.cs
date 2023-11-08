using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BasketController : Controller
    {

        public IActionResult Adds(int Id)
        {
            var Prod = ProductRepository.GetAll().Where(x => x.Id == Id).SingleOrDefault();
            ProductRepository.prodCart.Add(new Product(Prod.Id, Prod.Cost, Prod.Name, Prod.Description));
            return RedirectToActionPermanent("Index");
        }
        public IActionResult Index()
        {
            var r = ProductRepository.prodCart.GroupBy(x=>x.Id);
            var newListProd = new List<Product>();
            foreach (var item in r)
            {
                var key = item.Key;
                var resultSum = item.Sum(x => x.Cost);
                var name = item.First().Name;
                var description = item.First().Description;
                var count = item.Count();
                var newProd = new Product(key, resultSum, name, description, count);
                newListProd.Add(newProd);
            }
            return View(newListProd);
        }
    }
}
