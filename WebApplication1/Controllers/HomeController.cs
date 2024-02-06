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
        private readonly IUserAuth user;
        private readonly ICompareProductDbRepository compareProducts;
        private readonly IFavoriteProductDbRepository favoriteProducts;

        public HomeController(IProductRepository productRepository, ICompareProductDbRepository prodCompare, IUserAuth user, ICompareProductDbRepository compareProducts, IFavoriteProductDbRepository favoriteProducts)
        {
            this.productRepository = productRepository;
            this.prodCompare = prodCompare;
            this.user = user;
            this.compareProducts = compareProducts;
            this.favoriteProducts = favoriteProducts;
        }

        public IActionResult Index()
        {
            var products = productRepository.GetAll();
            var newProducts = ProductToProductView.Transform(products);
            var idCompareProds = compareProducts.GetCompareProducts(User.Identity.Name).Select(pr => pr.Id);
            var idFavoriteProds = favoriteProducts.GetFavoriteProducts(User.Identity.Name).Select(pr => pr.Id);
            //newProducts = newProducts.ForEach(prod =>
            //{
            //    if (idCompar)
            //});
            //foreach (var product in products)
            //{
            //    var prod = new ProductViewModel()
            //    {
            //        Id = product.Id,
            //        Name = product.Name,
            //        Cost = product.Cost,
            //        Description = product.Description,
            //    };
            //    if (idCompareProds.Contains(product.Id))
            //    {
            //        prod.Compare = true;
            //    }
            //    if (idFavoriteProds.Contains(product.Id))
            //    {
            //        prod.Favorite = true;
            //    }
            //    newProducts.Add(prod);
            //}
            return View(newProducts);
        }
        [Authorize]
        public IActionResult AddToComparsion(Guid productId)
        {
            //prodCompare.Add(productId, user.UserId);
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Compare()
        {
            //var products = compareProducts.GetCompareProducts(user.UserId);
            var prods = new List<ProductViewModel>();
            //foreach (var item in products)
            //{
            //    var newProd = new ProductViewModel();
            //    newProd.Id = item.Id;
            //    newProd.Name = item.Name;
            //    newProd.Cost = item.Cost;
            //    newProd.Description = item.Description;
            //    prods.Add(newProd);
            //}
            return View(prods);
        }
        [Authorize]
        public IActionResult DeleteFromComparsion(Guid productId)
        {
            //if (UserAuthSession.Auth)
            //{
            //    compareProducts.DeleteFromComparsion(productId, user.UserId);
            //}
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult AddToFavorite(Guid productId)
        {
            //if (UserAuthSession.Auth)
            //{
            //    favoriteProducts.AddProdToFavor(productId, user.UserId);
            //}
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult DeleteFromFavoriteIndex(Guid productId)
        {
            //if (UserAuthSession.Auth)
            //{
            //    favoriteProducts.DeleteFromFavorite(productId, user.UserId);
            //}
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult DeleteFromFavorite(Guid productId)
        {
            //if (UserAuthSession.Auth)
            //{
            //    favoriteProducts.DeleteFromFavorite(productId, user.UserId);
            //}
            return RedirectToAction("ShowFavorite");
        }
        [Authorize]
        public IActionResult ShowFavorite()
        {
            //var prodToShow = favoriteProducts.GetFavoriteProducts(user.UserId);
            var newListProd = new List<ProductViewModel>();
            //var idCompareProds = compareProducts.GetCompareProducts(user.UserId).Select(pr => pr.Id);
            //var idFavoriteProds = favoriteProducts.GetFavoriteProducts(user.UserId).Select(pr => pr.Id);
            //foreach (var product in prodToShow)
            //{
            //    var newProd = new ProductViewModel();
            //    newProd.Id = product.Id;
            //    newProd.Cost = product.Cost;
            //    newProd.Name = product.Name;
            //    newProd.Description = product.Description;
            //    if (idCompareProds.Contains(product.Id))
            //    {
            //        newProd.Compare = true;
            //    }
            //    if (idFavoriteProds.Contains(product.Id))
            //    {
            //        newProd.Favorite = true;
            //    }
            //    newListProd.Add(newProd);
            //}
            return View(newListProd);
        }

        [HttpPost]
        public IActionResult Search(string search)
        {
            search = search.ToLower();
            IEnumerable<Product> result;
            if (search == null || search == string.Empty)
            {
                result = null;
            }
            else
            {
                result = productRepository.GetAll().Where(x => x.Name.ToLower().Contains(search));
            }
            return View(result);
        }
    }
}