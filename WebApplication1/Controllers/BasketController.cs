using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;
using System.Collections.Immutable;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IBaskRepository bask;
        private readonly IOrderRepository orderRepository;
        private readonly ICartItemDbRepository carts;
        private readonly ICompareProductDbRepository compareProducts;
        private readonly IFavoriteProductDbRepository favoriteProducts;
        private readonly UserManager<User> users;
        private readonly IMapper mapper;

        public BasketController(IProductRepository productRepository, IBaskRepository bask, IOrderRepository orderRepository, ICartItemDbRepository carts, UserManager<User> users, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.bask = bask;
            this.orderRepository = orderRepository;
            this.carts = carts;
            this.users = users;
            this.mapper = mapper;
            this.mapper = mapper;
        }
        public async Task<IActionResult> AddsAsync(Guid productId)
        {
            var Prod = (await productRepository.GetAllAsync()).Where(x => x.Id == productId).First();
            var newProd = mapper.Map<ProductViewModel>(Prod);
            newProd.Images = await productRepository.GetProductImagesAsync(newProd.Id);
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

        public async Task<IActionResult> RegisterOrderAsync()
        {
            var resultBask = bask.GetResultProducts();
            ViewBag.Products = resultBask;
            ViewBag.ResultsCost = resultBask.Select(x => x.Cost * x.Count).Sum();
            var user = await users.FindByNameAsync(User.Identity.Name);
            ViewBag.user = user;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SuccessAsync(OrderViewModel order)
        {
            var orderDB = new Order();
            orderDB.User = await users.FindByNameAsync(User.Identity.Name);
            if (orderDB.User == null)
            {
                orderDB.Name = order.Name;
                orderDB.Number = order.Number;
                orderDB.Email = order.Email;
            }
            else
            {
                orderDB.Name = orderDB.User.UserName;
                orderDB.Number = orderDB.User.PhoneNumber;
                orderDB.Email = orderDB.User.Email;
            }
            var cart = bask.GetCart();
            orderDB.Total = bask.GetTotalCost();
            orderDB.Comments = order.Comments;
            orderDB.Address = order.Address;
            order.OrderCreation();
            orderDB.CreationDate = order.CreationDate;
            orderDB.CreationTime = order.CreationTime;
            orderDB.Status = order.Status;
            await orderRepository.AddAsync(orderDB);
            foreach (var product in cart)
            {
                await carts.AddAsync(orderDB.Id, product.Id);
            }
            bask.ClearResultProducts();
            bask.ClearCart();
            ViewBag.Products = await carts.GetOrdersCartsAsync(orderDB.Id);
            return View(orderDB);
        }
    }
}
