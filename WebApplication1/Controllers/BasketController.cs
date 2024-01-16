using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;
using System.Collections.Immutable;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class BasketController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IBaskRepository bask;
        private readonly IOrderRepository orderRepository;
        private readonly IUserAuth userAuthSession;
        private readonly IUserRegDbRepository users;

        public BasketController(IProductRepository productRepository, IBaskRepository bask, IOrderRepository orderRepository, IUserAuth userAuthSession, IUserRegDbRepository users)
        {
            this.productRepository = productRepository;
            this.bask = bask;
            this.orderRepository = orderRepository;
            this.userAuthSession = userAuthSession;
            this.users = users;
        }
        public IActionResult Adds(Guid productId)
        {
            var Prod = productRepository.GetAll().Where(x => x.Id == productId).First();
            var newProd = new ProductViewModel();
            newProd.Name = Prod.Name;
            newProd.Id = Prod.Id;
            newProd.Cost = Prod.Cost;
            newProd.Count = Prod.Count;
            newProd.Description = Prod.Description;
            newProd.Comparsion = Prod.Comparsion;
            newProd.Favorite = Prod.Favorite;
            bask.AddToCart(newProd);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            var k = bask.GetCart().GroupBy(x => x.Id);
            bask.ClearResultProducts();
            foreach (var item in k)
            {
                var id = item.Key;
                var name = item.First().Name;
                var count = item.Count();
                var cost = item.First().Cost;
                var descr = item.First().Description;
                var newProd = new ProductViewModel(id, cost, name, descr, count);
                bask.AddToResultProducts(newProd);
            }
            var relustProductsOrdered = bask.GetResultProducts();
            relustProductsOrdered = relustProductsOrdered.OrderBy(x => x.Name).ToList();
            return View(relustProductsOrdered);
        }

        public IActionResult Clear()
        {
            bask.ClearCart();
            return RedirectToAction("Index");
        }

        public IActionResult Plus(Guid Id)
        {
            var prod = bask.GetCart().Where(x => x.Id == Id).First();
            bask.AddToCart(prod);
            return RedirectToAction("Index");
        }

        public IActionResult Minus(Guid Id)
        {
            bask.RemoveFromCart(Id);
            return RedirectToAction("Index");
        }

        public IActionResult RegisterOrder()
        {
            var resultBask = bask.GetResultProducts();
            ViewBag.Products = resultBask;
            ViewBag.ResultsCost = resultBask.Select(x => x.Cost * x.Count).Sum();
            return View();
        }

        [HttpPost]
        public IActionResult Success(OrderViewModel order)
        {
            var orderDB = new Order();
            //orderDB.UserReg = users.Get(userAuthSession.UserId);
            var cart = bask.GetCart();
            foreach (var product in cart)
            {
                Product prod = new Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Cost = product.Cost,
                    Description = product.Description,
                    Count = product.Count,
                    Comparsion = product.Comparsion,
                    Favorite = product.Favorite,
                };
                //orderDB.Prods.Add(prod);
            }
            orderDB.Total = bask.GetTotalCost();
            orderDB.Comments = order.Comments;
            orderDB.Address = order.Address;
            bask.ClearResultProducts();
            bask.ClearCart();
            order.OrderCreation();
            orderDB.CreationDate = order.CreationDate;
            orderDB.CreationTime = order.CreationTime;
            orderDB.Status = order.Status;
            orderRepository.Add(orderDB);
            return View(orderDB);
        }
    }
}
