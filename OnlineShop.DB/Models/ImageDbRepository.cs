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
            return dataBaseContext.Images.FirstOrDefault(img => img.UserId == user.Id);
        }

        public void UpdateUserImage(User user, string Path)
        {
            var img = new Image()
            {
                UserId = user.Id,
                Path = Path,
            };
            var img2 = dataBaseContext.Images.FirstOrDefault(img => img.UserId == user.Id);
            if (img2 != null)
            {
                dataBaseContext.Images.Remove(img2);
            }
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
