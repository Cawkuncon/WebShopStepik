﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using System.Data;
using System.Linq.Expressions;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using WebApplication1.Areas.Admin.Models;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Area.Controlles
{
    [Area("Admin")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class HomeController : Controller
    {
        private UserManager<User> UsersRepository;
        private RoleManager<IdentityRole> RolesRepository;
        private IProductRepository productRepository;
        private IOrderRepository orderRepository;
        public HomeController(IProductRepository productRepository, IOrderRepository orderRepository, UserManager<User> users, RoleManager<IdentityRole> rolesRepository = null)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            UsersRepository = users;
            RolesRepository = rolesRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Orders()
        {
            var orders = orderRepository.GetAll();
            var listOrders = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                var user = UsersRepository.FindByNameAsync(order.Name).Result;
                OrderViewModel ordView = OrderTransformation.orderDBtoView(order, user);
                listOrders.Add(ordView);
            }
            return View(listOrders);
        }
        public IActionResult Users()
        {
            var usersToView = UsersRepository.Users;
            var newListUserRegViewModel = new List<UserRegViewModel>();
            foreach (var user in usersToView)
            {
                var userViewModel = new UserRegViewModel();
                userViewModel.Name = user.UserName;
                userViewModel.Age = user.Age;
                userViewModel.Email = user.Email;
                userViewModel.Number = user.PhoneNumber;
                userViewModel.UserId = user.Id;
                newListUserRegViewModel.Add(userViewModel);
            }
            return View(newListUserRegViewModel);
        }
        public IActionResult Products()
        {
            var products = productRepository.GetAll();
            var newProducts = ProductToProductView.TransformList(products);
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


        public IActionResult OrderInfo(Guid id, string name)
        {
            var order = orderRepository.GetOrder(id);
            var user = UsersRepository.FindByNameAsync(order.Name).Result;
            OrderViewModel orderInfo = OrderTransformation.orderDBtoView(order, user);
            return View(orderInfo);
        }


        [HttpPost]
        public IActionResult ChangeStatus(Guid idOrder, Status_Order Status)
        {
            orderRepository.UpdateStatus(idOrder, (int)Status);
            return RedirectToAction("OrderInfo", new { id = idOrder });
        }

        public IActionResult Roles()
        {
            var roles = RolesRepository.Roles;
            var listRoles = new List<RoleViewModel>();
            foreach (var role in roles)
            {
                var newRole = new RoleViewModel();
                newRole.Id = role.Id;
                newRole.Name = role.Name;
                listRoles.Add(newRole);
            }
            return View(listRoles);
        }

        public IActionResult DeleteRole(string Id)
        {
            var role = RolesRepository.Roles.Where(role => role.Id == Id).FirstOrDefault();
            RolesRepository.DeleteAsync(role).Wait();
            return RedirectToAction("Roles");
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToRepository(RoleViewModel role)
        {
            RolesRepository.CreateAsync(new IdentityRole(role.Name)).Wait();
            return RedirectToAction("Roles");
        }

        public IActionResult UserInfoCheck(string Name)
        {
            var user = UsersRepository.FindByNameAsync(Name).Result;
            var userViewModel = new UserRegViewModel();
            userViewModel.Name = user.UserName;
            userViewModel.Email = user.Email;
            userViewModel.Number = user.PhoneNumber;
            userViewModel.Age = user.Age;
            //userViewModel.UserId = user.Id;
            //userViewModel.Password = user.Password;
            //userViewModel.Password2 = user.Password2;
            var role = new RoleViewModel();
            
            //var userRole = rolesRepository.GetRole(user.RoleId);
            //if (userRole != null)
            //{
            //    role.Name = userRole.Name;
            //    role.Id = userRole.Id;
            //}
            //userViewModel.Role = role;
            //return View(userViewModel);
            return View(userViewModel);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(UserRegViewModel register)
        {
            if (register.Name == register.Password)
            {
                ModelState.AddModelError("", "Имя и пароль не должны совпадать");
            }
            if (ModelState.IsValid)
            {
                User user = new User { Email = register.Email, UserName = register.Name, PhoneNumber = register.Number, Age = register.Age };
                var result = UsersRepository.CreateAsync(user, register.Password).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Users");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(register);
        }
    }
}
