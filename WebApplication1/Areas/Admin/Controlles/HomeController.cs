using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
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
        private IWebHostEnvironment webHostEnvironment;
        private readonly IMapper mapper;
        public HomeController(IProductRepository productRepository, IOrderRepository orderRepository, UserManager<User> users, RoleManager<IdentityRole> rolesRepository, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            UsersRepository = users;
            RolesRepository = rolesRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> OrdersAsync()
        {
            var orders = await orderRepository.GetAllAsync();
            var listOrders = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                var user = await UsersRepository.FindByNameAsync(order.Name);
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
        public async Task<IActionResult> ProductsAsync()
        {
            var products = await productRepository.GetAllAsync();
            var newProducts = mapper.Map<List<ProductViewModel>>(products);
            return View(newProducts);
        }

        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            Product product = await productRepository.GetProductAsync(id);

            foreach (var image in product.Images)
            {
                string path = Path.Combine(webHostEnvironment.WebRootPath + image.Path);
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
            }
            await productRepository.DeleteAsync(id);
            return RedirectToAction("Products");
        }

        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(Product product)
        {
            await productRepository.AddProdAsync(product);
            return RedirectToAction("Products");
        }

        public async Task<IActionResult> EditProductAsync(Guid id)
        {
            var prod = (await productRepository.GetAllAsync()).Find(x => x.Id == id);
            return View(prod);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductAsync(Product product, Guid id)
        {
            product.Id = id;
            await productRepository.UpdateProdAsync(product);
            return RedirectToAction("Products");
        }


        public async Task<IActionResult> OrderInfoAsync(Guid id)
        {
            var order = await orderRepository.GetOrderAsync(id);
            var user = new User();
            if (order.User == null)
            {
                user = null;
            }
            else
            {
                user = await UsersRepository.FindByNameAsync(order.User.UserName);
            }
            OrderViewModel orderInfo = OrderTransformation.orderDBtoView(order, user);
            orderInfo.Products = mapper.Map<List<ProductViewModel>>(await orderRepository.GetAllProductsAsync(order.Id));
            return View(orderInfo);
        }


        [HttpPost]
        public async Task<IActionResult> ChangeStatusAsync(Guid idOrder, Status_Order Status)
        {
            await orderRepository.UpdateStatusAsync(idOrder, (int)Status);
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

        public async Task<IActionResult> DeleteRoleAsync(string Id)
        {
            var role = RolesRepository.Roles.Where(role => role.Id == Id).FirstOrDefault();
            await RolesRepository.DeleteAsync(role);
            return RedirectToAction("Roles");
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToRepositoryAsync(RoleViewModel role)
        {
            await RolesRepository.CreateAsync(new IdentityRole(role.Name));
            return RedirectToAction("Roles");
        }

        public async Task<IActionResult> UserInfoCheckAsync(string Name)
        {
            var user = await UsersRepository.FindByNameAsync(Name);
            var userViewModel = new UserRegViewModel();
            userViewModel.Name = user.UserName;
            userViewModel.Email = user.Email;
            userViewModel.Number = user.PhoneNumber;
            userViewModel.Age = user.Age;
            userViewModel.UserId = user.Id;
            var role = new RoleViewModel();
            var usersRoles = await UsersRepository.GetRolesAsync(user);
            var firstRole = usersRoles.First();
            var rol = await RolesRepository.FindByNameAsync(firstRole);
            if (rol != null)
            {
                role.Name = rol.Name;
                role.Id = rol.Id;
            }
            userViewModel.Role = role;
            return View(userViewModel);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync(UserRegViewModel register)
        {
            if (register.Name == register.Password)
            {
                ModelState.AddModelError("", "Имя и пароль не должны совпадать");
            }
            if (ModelState.IsValid)
            {
                User user = new User { Email = register.Email, UserName = register.Name, PhoneNumber = register.Number, Age = register.Age };
                var result = await UsersRepository.CreateAsync(user, register.Password);
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

        public async Task<IActionResult> EditProductImageAsync(Guid id)
        {
            var prod = await productRepository.GetProductAsync(id);
            prod.Images = productRepository.GetProductImages(id);
            return View(prod);
        }

        [HttpPost]
        public async Task<IActionResult> EditProductImageAsync(Guid id, IFormFile formFile)
        {
            var prod = await productRepository.GetProductAsync(id);
            var model = new CreateImageViewModel()
            {
                Name = prod.Name,
                Id = prod.Id,
            };
            if (model != null && formFile != null)
            {
                model.formFile = formFile;
                string prodPath = Path.Combine(webHostEnvironment.WebRootPath + "/img/products/");
                if (!Directory.Exists(prodPath))
                {
                    Directory.CreateDirectory(prodPath);
                }
                var fileName = Guid.NewGuid().ToString() + "." + model.formFile.FileName.Split(".").Last();
                using (var fileStream = new FileStream(prodPath + fileName, FileMode.Create))
                {
                    model.formFile.CopyTo(fileStream);
                }
                await productRepository.UpdateProdImageAsync(model.Id, "/img/products/" + fileName);
            }
            return RedirectToAction("Products");
        }
        public async Task<IActionResult> DeleteImageAsync(Guid productId, string imgSrc)
        {
            await productRepository.DeleteImageAsync(productId, imgSrc);
            string path = Path.Combine(webHostEnvironment.WebRootPath + imgSrc);
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            return RedirectToAction("EditProductImage", new { id = productId });
        }
    }
}
