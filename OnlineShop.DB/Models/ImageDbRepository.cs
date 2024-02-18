using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB.Models
{
    public class ImageDbRepository : IImageDbRepository
    {
        private DataBaseContext dataBaseContext;
        public ImageDbRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }
        public Image GetUserImage(User user)
        {
            return dataBaseContext.Images.FirstOrDefault(img => img.User.Id == user.Id);
        }

        public void UpdateUserImage(User user, string Path)
        {
            var img = new Image()
            {
                User = user,
                Path = Path,
            };
            dataBaseContext.Images.Add(img);
            dataBaseContext.SaveChanges();
        }
    }

    public interface IImageDbRepository
    {
        public Image GetUserImage(User user);
        public void UpdateUserImage(User user, string Path);
    }
}
