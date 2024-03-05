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
        public async Task<IActionResult> IndexAsync()
        {
            var user = await UsersRepository.FindByNameAsync(User.Identity.Name);
            var orders = await ordersRepository.GetAllUserOrdersAsync(user.Id);
            var image = await imageDbRepository.GetUserImageAsync(user);
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
        public async Task<IActionResult> EditPasswordAsync(ChangePassword changePassword)
        {
            var user = await UsersRepository.FindByNameAsync(changePassword.Name);
            if (changePassword.Name == changePassword.Password)
            {
                ModelState.AddModelError("", "Пароли не должен совпадать с именем");
            }
            if (ModelState.IsValid)
            {
                var newPasswordHash = UsersRepository.PasswordHasher.HashPassword(user, changePassword.Password);
                user.PasswordHash = newPasswordHash;
                await UsersRepository.UpdateAsync(user);
                return RedirectToAction("Index", "Home");
            }
            return View(changePassword);
        }

        public async Task<IActionResult> EditUserInfoAsync(string Name)
        {
            var user = await UsersRepository.FindByNameAsync(Name);
            var userViewModel = new UserRegViewModel();
            userViewModel.Name = user.UserName;
            userViewModel.Email = user.Email;
            userViewModel.Number = user.PhoneNumber;
            userViewModel.Age = user.Age;
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserInfoAsync(UserRegViewModel user, string Name)
        {
            var userDB = await UsersRepository.FindByNameAsync(Name);
            userDB.Age = user.Age;
            userDB.PhoneNumber = user.Number;
            userDB.Email = user.Email;
            UsersRepository.UpdateAsync(userDB).Wait();
            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> EditUserImageAsync(string Name)
        {
            var user = await UsersRepository.FindByNameAsync(Name);
            ViewBag.Image = await imageDbRepository.GetUserImageAsync(user);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserImageAsync(string Name, IFormFile formFile)
        {
            var user = await UsersRepository.FindByNameAsync(Name);
            var id = Guid.NewGuid();
            Guid.TryParse(user.Id, out id);
            var model = new CreateImageViewModel()
            {
                Name = user.UserName,
                Id = id,
            };
            if (model != null && formFile != null)
            {
                model.formFile = formFile;
                string userPath = Path.Combine(webHostEnvironment.WebRootPath + "/img/users/");
                if (!Directory.Exists(userPath))
                {
                    Directory.CreateDirectory(userPath);
                }
                var fileName = model.Id.ToString() + "." + model.formFile.FileName.Split(".").Last();
                var file = new FileInfo(webHostEnvironment.WebRootPath + "/img/users/" + fileName);
                if (file.Exists)
                {
                    file.Delete();
                }
                using (var fileStream = new FileStream(userPath + fileName, FileMode.Create))
                {
                    model.formFile.CopyTo(fileStream);
                }
                await imageDbRepository.UpdateUserImageAsync(user, "/img/users/" + fileName);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteImageAsync(string name)
        {
            var user = await UsersRepository.FindByNameAsync(name);
            await imageDbRepository.DeleteImageAsync(user);
            return RedirectToAction(nameof(EditUserImageAsync), new { Name = name });
        }
    }
}
