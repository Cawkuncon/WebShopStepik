using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository productRepository;

        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var products = productRepository.GetAll();

            return View(products);
        }

        public IActionResult AddToComparsion(Guid productId)
        {
            var prodToCompare = productRepository.GetAll().Where(x => x.Id == productId).First();
            prodToCompare.Comparsion = !prodToCompare.Comparsion;
            return RedirectToAction("Index");
        }

        public IActionResult Compare()
        {
            var products = productRepository.GetAll().Where(x => x.Comparsion).OrderBy(x => x.Name).ToList();
            return View(products);
        }

        public IActionResult DeleteFromComparsion(Guid productId)
        {
            var prodToCompare = productRepository.GetAll().Where(x => x.Id == productId).First();
            prodToCompare.Comparsion = !prodToCompare.Comparsion;
            return RedirectToAction("Compare");
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