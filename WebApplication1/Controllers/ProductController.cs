using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
		private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public IActionResult Index(Guid id)
        {
            var products = productRepository.GetAll().ToList();
            var prods = mapper.Map<List<ProductViewModel>>(products);
            prods.ForEach(prod => prod.Images = productRepository.GetProductImages(prod.Id));
            var product = prods.FirstOrDefault(pr => pr.Id == id);
            return View(product);
        }
    }
}
