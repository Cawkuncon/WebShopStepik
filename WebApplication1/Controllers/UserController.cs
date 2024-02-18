using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;
using WebApplication1.Areas.Admin.Models;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserManager<User> UsersRepository;
        private RoleManager<IdentityRole> RolesRepository;
        private IOrderRepository ordersRepository;
        private IWebHostEnvironment webHostEnvironment;
        private IImageDbRepository imageDbRepository;
        public UserController(RoleManager<IdentityRole> rolesRepository, UserManager<User> usersRepository, IOrderRepository ordersRepository, IWebHostEnvironment webHostEnvironment, IImageDbRepository imageDbRepository)
        {
            RolesRepository = rolesRepository;
            UsersRepository = usersRepository;
            this.ordersRepository = ordersRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.imageDbRepository = imageDbRepository;
        }
        public IActionResult Index()
        {
            var user = UsersRepository.FindByNameAsync(User.Identity.Name).Result;
            var orders = ordersRepository.GetAllUserOrders(user.Id);
            var image = imageDbRepository.GetUserImage(user);
            var listOrders = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                OrderViewModel ordView = OrderTransformation.orderDBtoView(order, user);
                listOrders.Add(ordView);
            }
            ViewBag.Orders = listOrders;
            ViewBag.Image = image;
            return View(user);
        }

        public IActionResult EditPassword(string Name)
        {
            ViewBag.Name = Name;
            return View();
        }

        [HttpPost]
        public IActionResult EditPassword(ChangePassword changePassword)
        {
            var user = UsersRepository.FindByNameAsync(changePassword.Name).Result;
            if (changePassword.Name == changePassword.Password)
            {
                ModelState.AddModelError("", "Пароли не должен совпадать с именем");
            }
            if (ModelState.IsValid)
            {
                var newPasswordHash = UsersRepository.PasswordHasher.HashPassword(user, changePassword.Password);
                user.PasswordHash = newPasswordHash;
                UsersRepository.UpdateAsync(user).Wait();
                return RedirectToAction("Index", "Home");
            }
            return View(changePassword);
        }

        public IActionResult EditUserInfo(string Name)
        {
            var user = UsersRepository.FindByNameAsync(Name).Result;
            var userViewModel = new UserRegViewModel();
            userViewModel.Name = user.UserName;
            userViewModel.Email = user.Email;
            userViewModel.Number = user.PhoneNumber;
            userViewModel.Age = user.Age;
            return View(userViewModel);
        }

        [HttpPost]
        public IActionResult EditUserInfo(UserRegViewModel user, string Name)
        {
            var userDB = UsersRepository.FindByNameAsync(Name).Result;
            userDB.Age = user.Age;
            userDB.PhoneNumber = user.Number;
            userDB.Email = user.Email;
            UsersRepository.UpdateAsync(userDB).Wait();
            return RedirectToAction("Index", "User");
        }

        public IActionResult EditUserImage(string Name)
        {
            var user = UsersRepository.FindByNameAsync(Name).Result;
            ViewBag.Image = imageDbRepository.GetUserImage(user);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUserImage(string Name, IFormFile formFile)
        {
            var user = UsersRepository.FindByNameAsync(Name).Result;
            var id = Guid.NewGuid();
            Guid.TryParse(user.Id, out id);
            var model = new CreateImageViewModel()
            {
                Name = user.UserName,
                Id = id,
            };
            if (model != null && formFile != null)
            {
                model.formFile= formFile;
                string userPath = Path.Combine(webHostEnvironment.WebRootPath + "/img/users/");
                if (!Directory.Exists(userPath))
                {
                    Directory.CreateDirectory(userPath);
                }
                var fileName = model.Id.ToString() +"." + model.formFile.FileName.Split(".").Last();
                var file = new FileInfo(webHostEnvironment.WebRootPath + "/img/users/" + fileName);
                if (file.Exists)
                {
                    file.Delete();
                }
                using (var fileStream = new FileStream(userPath + fileName, FileMode.Create))
                {
                    model.formFile.CopyTo(fileStream);
                }
                imageDbRepository.UpdateUserImage(user, "/img/users/" + fileName);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteImage(string name)
        {
            var user = UsersRepository.FindByNameAsync(name).Result;
            imageDbRepository.DeleteImage(user);
            return RedirectToAction(nameof(EditUserImage), new {Name = name});
        }
    }
}
