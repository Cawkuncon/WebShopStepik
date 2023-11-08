using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            var r = ProductRepository.prodCart;
            return View(r);
        }

        public IActionResult Add(int Id)
        {
            var Prod = new ProductRepository().GetAll().Where(x=> x.Id == Id).SingleOrDefault();
            ProductRepository.prodCart.Add(Prod);
            return View("Views/Basket/Index.cshtml", ProductRepository.prodCart);
        }
    }
}
