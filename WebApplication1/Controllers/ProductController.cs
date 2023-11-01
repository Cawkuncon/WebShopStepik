using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        readonly ProductRepository productsRepository;
        public ProductController()
        {
            productsRepository = new ProductRepository();
        }
        public IActionResult Index(string id)
        {
            var ID = -1;
            if (int.TryParse(id, out var productId))
            {
                ID = productId;
            }
            var products = productsRepository.GetAll();
            var product = products.FirstOrDefault(pr => pr.Id == ID);
            return View(product);
        }
    }
}
