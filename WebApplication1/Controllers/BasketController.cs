using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class BasketController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IBaskRepository bask;
        private IOrder order;
        private readonly IOrderRepository orderRepository;

        public BasketController(IProductRepository productRepository, IBaskRepository bask, IOrder order, IOrderRepository orderRepository)
        {
            this.productRepository = productRepository;
            this.bask = bask;
            this.order = order;
            this.orderRepository = orderRepository;
        }
        public IActionResult Adds(string productId)
        {
            var id = int.Parse(productId);
            var Prod = productRepository.GetAll().Where(x => x.Id == id).First();
            bask.AddToCart(Prod);
            return RedirectToAction("Index");
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
                var newProd = new Product(id, cost, name, descr, count);
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

        public IActionResult Plus(int Id)
        {
            var prod = bask.GetCart().Where(x => x.Id == Id).First();
            bask.AddToCart(prod);
            return RedirectToAction("Index");
        }

        public IActionResult Minus(int Id)
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
        public IActionResult Success(Order order)
        {
            this.order = new Order();
            this.order.Name = order.Name;
            this.order.Number = order.Number;
            this.order.Email = order.Email;
            var cart = bask.GetCart();
            this.order.Products = new List<Product>();
            this.order.Products.AddRange(cart);
            this.order.Total = bask.GetTotalCost();
            this.order.Comments = order.Comments;
            this.order.Address = order.Address;
            bask.ClearResultProducts();
            bask.ClearCart();
            var orderToAdd = (Order)this.order;
            orderToAdd.OrderCreation();
            orderRepository.Add(orderToAdd);
            return View(this.order);
        }
    }
}
