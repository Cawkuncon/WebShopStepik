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

        public HomeController(IProductRepository productRepository, ICompareProductDbRepository prodCompare, IUserAuth user, ICompareProductDbRepository compareProducts)
        {
            this.productRepository = productRepository;
            this.prodCompare = prodCompare;
            this.user = user;
            this.compareProducts = compareProducts;
        }

        public IActionResult Index()
        {
            var products = productRepository.GetAll();
            var newProducts = new List<ProductViewModel>();
            var idCompareProds = compareProducts.GetCompareProducts(user.UserId);
            foreach (var product in products)
            {
                var prod = new ProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Cost = product.Cost,
                    Description = product.Description,
                };
                if (idCompareProds.Contains(product.Id))
                {
                    prod.Compare = true;
                }
                newProducts.Add(prod);
            }
            return View(newProducts);
        }

        public IActionResult AddToComparsion(Guid productId)
        {
            if (UserAuthSession.Auth)
            {
                prodCompare.Add(productId, user.UserId);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Compare()
        {
            var products = productRepository.GetAll().Where(x => x.Comparsion).OrderBy(x => x.Name).ToList();
            return View(products);
        }

        public IActionResult DeleteFromComparsion(Guid productId)
        {
            compareProducts.DeleteFromComparsion(productId, user.UserId);
            return RedirectToAction("Index");
        }


        public IActionResult AddToFavorite(Guid productId)
        {
            var prodToAdd = productRepository.GetAll().Where(x => x.Id == productId).First();
            prodToAdd.Favorite = !prodToAdd.Favorite;
            return RedirectToAction("Index");
        }

        public IActionResult DeleteFromFavoriteIndex(Guid id)
        {
            var prodToDelete = productRepository.GetAll().Where(x => x.Id == id).First();
            prodToDelete.Favorite = !prodToDelete.Favorite;
            return RedirectToAction("Index");
        }

        public IActionResult DeleteFromFavorite(Guid id)
        {
            var prodToDelete = productRepository.GetAll().Where(x => x.Id == id).First();
            prodToDelete.Favorite = !prodToDelete.Favorite;
            return RedirectToAction("ShowFavorite");
        }

        public IActionResult ShowFavorite()
        {
            var prodToShow = productRepository.GetAll().Where(x => x.Favorite).ToList();
            return View(prodToShow);
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