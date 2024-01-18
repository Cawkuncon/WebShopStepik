using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;
using System.Data;
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
        private readonly IUserRegDbRepository UsersRepository;
        public HomeController(IProductRepository productRepository, IOrderRepository orderRepository, IRolesRepository rolesRepository, IUserRegDbRepository users)
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
            var usersToView = UsersRepository.GetAll();
            var newListUserRegViewModel = new List<UserRegViewModel>();
            foreach (var user in usersToView)
            {
                var userViewModel = new UserRegViewModel();
                userViewModel.Name = user.Name;
                userViewModel.Email = user.Email;
                userViewModel.Number = user.Number;
                userViewModel.Age = user.Age;
                userViewModel.UserId = user.Id;
                userViewModel.Password = user.Password;
                userViewModel.Password2 = user.Password2;
                newListUserRegViewModel.Add(userViewModel);
            }
            return View(newListUserRegViewModel);
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


        //public IActionResult OrderInfo(int id)
        //{
        //    var order = orderRepository.GetAll().FindLast(ord => ord.Id1 == id);
        //    return View(order);
        //}

        //[HttpPost]
        //public IActionResult ChangeStatus(int idOrder, Status_Order Status)
        //{
        //    var order = orderRepository.GetAll().FirstOrDefault(ord => ord.Id1 == idOrder);
        //    order.Status = Status;
        //    return RedirectToAction("OrderInfo", new { id = idOrder });
        //}

        public IActionResult Roles()
        {
            var roles = rolesRepository.GetAll();
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

        public IActionResult DeleteRole(Guid Id)
        {
            rolesRepository.DeleteRole(Id);
            return RedirectToAction("Roles");
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToRepository(RoleViewModel role)
        {
            var roleDb = new Role();
            roleDb.Id = role.Id;
            roleDb.Name = role.Name;
            rolesRepository.AddRole(roleDb);
            return RedirectToAction("Roles");
        }

        public IActionResult UserInfoCheck(Guid Id)
        {
            var user = UsersRepository.Get(Id);
            var userViewModel = new UserRegViewModel();
            userViewModel.Name = user.Name;
            userViewModel.Email = user.Email;
            userViewModel.Number = user.Number;
            userViewModel.Age = user.Age;
            userViewModel.UserId = user.Id;
            userViewModel.Password = user.Password;
            userViewModel.Password2 = user.Password2;
            if (user.RoleId != null)
            {
                var role = new RoleViewModel();
                var userRole = rolesRepository.GetRole(user.RoleId);
                role.Name = userRole.Name;
                role.Id = userRole.Id;
                userViewModel.Role = role;
            }
            return View(userViewModel);
        }

        public IActionResult AddUser()
        {
            return View();
        }
    }
}
