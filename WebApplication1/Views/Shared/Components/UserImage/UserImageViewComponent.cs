using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;

namespace WebApplication1.Views.Shared.Components.UserImage
{
    public class UserImageViewComponent : ViewComponent
    {
        private UserManager<User> UsersRepository;
        private IImageDbRepository imageDbRepository;
        public UserImageViewComponent(UserManager<User> usersRepository, IImageDbRepository imageDbRepository)
        {
            UsersRepository = usersRepository;
            this.imageDbRepository = imageDbRepository;
        }

        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = UsersRepository.FindByNameAsync(User.Identity.Name).Result;
                var image = imageDbRepository.GetUserImage(user);
                return View("UserImage", image);
            }
            return View("UserImage", new Image());
        }
    }
}
