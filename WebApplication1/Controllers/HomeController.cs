using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;
using System.Diagnostics;
using System.Linq.Expressions;
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

        public HomeController(IProductRepository productRepository, ICompareProductDbRepository prodCompare, ICompareProductDbRepository compareProducts, IFavoriteProductDbRepository favoriteProducts, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.prodCompare = prodCompare;
            this.compareProducts = compareProducts;
            this.favoriteProducts = favoriteProducts;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = productRepository.GetAll();
            var newProducts = mapper.Map<List<ProductViewModel>>(products);
            newProducts.ForEach(prod => prod.Images = productRepository.GetProductImages(prod.Id));
            if (User.Identity.IsAuthenticated)
            {
                var idCompareProds = compareProducts.GetCompareProducts(User.Identity.Name).Select(pr => pr.Id);
                var idFavoriteProds = favoriteProducts.GetFavoriteProducts(User.Identity.Name).Select(pr => pr.Id);
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
        public IActionResult AddToComparsion(Guid productId)
        {
            prodCompare.Add(productId, User.Identity.Name);
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Compare()
        {
            var products = compareProducts.GetCompareProducts(User.Identity.Name);
            var prods = mapper.Map<List<ProductViewModel>>(products);
            return View(prods);
        }
        [Authorize]
        public IActionResult DeleteFromComparsion(Guid productId)
        {
            compareProducts.DeleteFromComparsion(productId, User.Identity.Name);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult AddToFavorite(Guid productId)
        {
            favoriteProducts.AddProdToFavor(productId, User.Identity.Name);
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult DeleteFromFavoriteIndex(Guid productId)
        {
            favoriteProducts.DeleteFromFavorite(productId, User.Identity.Name);
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult DeleteFromFavorite(Guid productId)
        {
            favoriteProducts.DeleteFromFavorite(productId, User.Identity.Name);
            return RedirectToAction("ShowFavorite");
        }
        [Authorize]
        public IActionResult ShowFavorite()
        {
            var prodToShow = favoriteProducts.GetFavoriteProducts(User.Identity.Name);
            var newListProd = mapper.Map<List<ProductViewModel>>(prodToShow);
            newListProd.ForEach(prod => prod.Images = productRepository.GetProductImages(prod.Id));
            var idCompareProds = compareProducts.GetCompareProducts(User.Identity.Name).Select(pr => pr.Id);
            var idFavoriteProds = favoriteProducts.GetFavoriteProducts(User.Identity.Name).Select(pr => pr.Id);
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
        public IActionResult Search(string search)
        {
            if (search == null || search == string.Empty)
            {
                return RedirectToAction(nameof(Index));
            }
            search = search.ToLower();
            var result = productRepository.GetAll().Where(x => x.Name.ToLower().Contains(search));
            var resultProductViewModel = mapper.Map<List<ProductViewModel>>(result.ToList());
            resultProductViewModel.ForEach(product => product.Images = productRepository.GetProductImages(product.Id));
            return View(resultProductViewModel);
        }
    }
}