﻿using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using WebApplication1.Areas.Admin.Models;
using WebApplication1.Models;

namespace WebApplication1.Area.Controlles
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private IProductRepository productRepository;
        private IOrderRepository orderRepository;
        private IRolesRepository rolesRepository;
        private readonly IUserRepository UsersRepository;
        public HomeController(IProductRepository productRepository, IOrderRepository orderRepository, IRolesRepository rolesRepository, IUserRepository users)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.rolesRepository = rolesRepository;
            this.UsersRepository = users;
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
            var usersToView = UsersRepository.GetUsers();
            return View(usersToView);
        }
        public IActionResult Products()
        {
            var products = productRepository.GetAll();
            var newProducts = new List<ProductViewModel>();
            foreach (var product in products)
            {
                var prod = new ProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Cost = product.Cost,
                    Description = product.Description,
                    Favorite = product.Favorite,
                    Comparsion = product.Comparsion,
                    Count = product.Count,
                };
                newProducts.Add(prod);
            }
            return View(newProducts);
        }

        public IActionResult DeleteProduct(Guid id)
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
            productRepository.AddProd(product);
            return RedirectToAction("Products");
        }

        public IActionResult EditProduct(Guid id)
        {
            var prod = productRepository.GetAll().Find(x => x.Id == id);
            return View(prod);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product, Guid id)
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
            var order = orderRepository.GetAll().FirstOrDefault(ord => ord.Id1 == idOrder);
            order.Status = Status;
            return RedirectToAction("OrderInfo", new { id = idOrder });
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

        public IActionResult UserInfoCheck(string Name)
        {
            var user = UsersRepository.GetUser(Name);
            return View(user);
        }

        public IActionResult AddUser()
        {
            return View();
        }
    }
}
