using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository productRepository;
        private IOrderRepository orderRepository;
        public AdminController(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Orders()
        {
            var orders = orderRepository.GetAll();
            return View(orders);
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Roles()
        {
            return View();
        }
        public IActionResult Products()
        {
            return View(productRepository.GetAll());
        }

        public IActionResult DeleteProduct(int id)
        {
            productRepository.Delete(id);
            return RedirectToAction("Products");
        }

        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (productRepository.GetAll().Count == 0)
            {
                product.Id = 0;
            }
            else
            {
                product.Id = productRepository.GetAll().Last().Id + 1;
            }
            productRepository.AddProd(product);
            return RedirectToAction("Products");
        }

        public IActionResult EditProduct(int id)
        {
            var prod = productRepository.GetAll().Find(x => x.Id == id);
            return View(prod);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product, int id)
        {
            product.Id = id;
            productRepository.UpdateProd(product);
            return RedirectToAction("Products");
        }


        public IActionResult OrderInfo(int id)
        {
            var order = orderRepository.GetAll().FindLast(ord => ord.Id1 == id);
            return View(order);
        }
    }
}
