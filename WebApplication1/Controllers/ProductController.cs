using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
		private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;

        public ProductController(IProductRepository productRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
        }
        public IActionResult Index(Guid id)
        {
            memoryCache.TryGetValue(id, out Product product);
            if (product == null)
            {
                memoryCache.TryGetValue(Constants.ProductsCache, out List<Product> products);
                if (products != null)
                {
                    product = products.FirstOrDefault(pr => pr.Id == id);
                    if (product == null)
                    {
                        return NotFound();
                    }
                    var productView = mapper.Map<ProductViewModel>(product);
                    productView.Images = productRepository.GetProductImages(id);
                    return View(productView);
                }
            }
            else
            {
                var productView = mapper.Map<ProductViewModel>(product);
                productView.Images = productRepository.GetProductImages(id);
                return View(productView);
            }
            return View();
        }
    }
}
