using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
		private readonly IProductRepository productRepository;

		public ProductController(IProductRepository productRepository)
        {
			this.productRepository = productRepository;
		}
        public IActionResult Index(Guid id)
        {
            var products = productRepository.GetAll().ToList();
            var prods = ProductToProductView.TransformList(products);
            var product = prods.FirstOrDefault(pr => pr.Id == id);
            return View(product);
        }
    }
}
