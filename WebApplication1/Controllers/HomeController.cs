using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICompareProductDbRepository prodCompare;
        private readonly ICompareProductDbRepository compareProducts;
        private readonly IFavoriteProductDbRepository favoriteProducts;
        private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;

        public HomeController(IProductRepository productRepository, ICompareProductDbRepository prodCompare, ICompareProductDbRepository compareProducts, IFavoriteProductDbRepository favoriteProducts, IMapper mapper, IMemoryCache memoryCache)
        {
            this.productRepository = productRepository;
            this.prodCompare = prodCompare;
            this.compareProducts = compareProducts;
            this.favoriteProducts = favoriteProducts;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index()
        {
            memoryCache.TryGetValue<List<Product>>(Constants.ProductsCache, out var products);
            if (products == null)
            {
                products = await productRepository.GetAllAsync();
                var newCacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                };
                memoryCache.Set(Constants.ProductsCache, products, newCacheOptions);
                foreach (var product in products)
                {
                    memoryCache.Set(product.Id, product, newCacheOptions);
                }
            }
            var newProducts = mapper.Map<List<ProductViewModel>>(products);
            newProducts.ForEach(prod => prod.Images = productRepository.GetProductImages(prod.Id));
            if (User.Identity.IsAuthenticated)
            {
                var idCompareProds = (await compareProducts.GetCompareProductsAsync(User.Identity.Name)).Select(pr => pr.Id);
                var idFavoriteProds = (await favoriteProducts.GetFavoriteProductsAsync(User.Identity.Name)).Select(pr => pr.Id);
                newProducts.ForEach(prod =>
                {
                    if (idCompareProds != null)
                    {
                        if (idCompareProds.Contains(prod.Id))
                        {
                            prod.ChangeCompare();
                        }
                    }
                    if (idFavoriteProds != null)
                    {
                        if (idFavoriteProds.Contains(prod.Id))
                        {
                            prod.ChangeFavorite();
                        }
                    }
                });
            }
            return View(newProducts);
        }
        [Authorize]
        public async Task<IActionResult> AddToComparsionAsync(Guid productId)
        {
            await prodCompare.AddAsync(productId, User.Identity.Name);
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> CompareAsync()
        {
            var products = await compareProducts.GetCompareProductsAsync(User.Identity.Name);
            var prods = mapper.Map<List<ProductViewModel>>(products);
            return View(prods);
        }
        [Authorize]
        public async Task<IActionResult> DeleteFromComparsionAsync(Guid productId)
        {
            await compareProducts.DeleteFromComparsionAsync(productId, User.Identity.Name);
            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> AddToFavoriteAsync(Guid productId)
        {
            await favoriteProducts.AddProdToFavorAsync(productId, User.Identity.Name);
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> DeleteFromFavoriteIndexAsync(Guid productId)
        {
            await favoriteProducts.DeleteFromFavoriteAsync(productId, User.Identity.Name);
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> DeleteFromFavoriteAsync(Guid productId)
        {
            await favoriteProducts.DeleteFromFavoriteAsync(productId, User.Identity.Name);
            return RedirectToAction("ShowFavorite");
        }
        [Authorize]
        public async Task<IActionResult> ShowFavoriteAsync()
        {
            var prodToShow = await favoriteProducts.GetFavoriteProductsAsync(User.Identity.Name);
            var newListProd = mapper.Map<List<ProductViewModel>>(prodToShow);
            newListProd.ForEach(prod => prod.Images = productRepository.GetProductImages(prod.Id));
            var idCompareProds = (await compareProducts.GetCompareProductsAsync(User.Identity.Name)).Select(pr => pr.Id);
            var idFavoriteProds = (await favoriteProducts.GetFavoriteProductsAsync(User.Identity.Name)).Select(pr => pr.Id);
            newListProd.ForEach(prod =>
            {
                if (idCompareProds != null)
                {
                    if (idCompareProds.Contains(prod.Id))
                    {
                        prod.ChangeCompare();
                    }
                }
                if (idFavoriteProds != null)
                {
                    if (idFavoriteProds.Contains(prod.Id))
                    {
                        prod.ChangeFavorite();
                    }
                }
            });
            return View(newListProd);
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(string search)
        {
            if (search == null || search == string.Empty)
            {
                return RedirectToAction(nameof(Index));
            }
            search = search.ToLower();
            memoryCache.TryGetValue<IEnumerable<Product>>(Constants.ProductsCache, out var result);
            if (result == null)
            {
                result = (await productRepository.GetAllAsync()).Where(x => x.Name.ToLower().Contains(search));
            }
            else
            {
                result = result.Where(x => x.Name.ToLower().Contains(search));
            }
            var resultProductViewModel = mapper.Map<List<ProductViewModel>>(result.ToList());
            resultProductViewModel.ForEach(product => product.Images = productRepository.GetProductImages(product.Id));
            return View(resultProductViewModel);
        }
    }
}