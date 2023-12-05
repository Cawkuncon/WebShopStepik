using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository productRepository;
        private IOrderRepository orderRepository;
        private IRolesRepository rolesRepository;
        public AdminController(IProductRepository productRepository, IOrderRepository orderRepository, IRolesRepository rolesRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.rolesRepository = rolesRepository;  
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

        [HttpPost]
        public IActionResult ChangeStatus(int idOrder, Status_Order Status)
        {
            var order = orderRepository.GetAll().FirstOrDefault(ord=> ord.Id1 == idOrder);
            order.Status = Status;
            return RedirectToAction("OrderInfo", new {id = idOrder});
        }

        public IActionResult Roles()
        {
            var roles = rolesRepository.GetAll();
            return View(roles);
        }

        public IActionResult DeleteRole(int id)
        {
            var roleToDelete = rolesRepository.GetAll().FirstOrDefault(rol => rol.Id == id);
            rolesRepository.DeleteRole(roleToDelete);
            return RedirectToAction("Roles");
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToRepository(Role roleToAdd) 
        {
            rolesRepository.AddRole(roleToAdd);
            return RedirectToAction("Roles");
        }
    }
}
