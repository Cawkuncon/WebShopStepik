using Microsoft.EntityFrameworkCore;
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
        public async Task<Image> GetUserImageAsync(User user) => await dataBaseContext.Images.FirstOrDefaultAsync(img => img.UserId == user.Id);

        public async Task UpdateUserImageAsync(User user, string Path)
        {
            var img = new Image()
            {
                UserId = user.Id,
                Path = Path,
            };
            var img2 = await dataBaseContext.Images.FirstOrDefaultAsync(img => img.UserId == user.Id);
            if (img2 != null)
            {
                dataBaseContext.Images.Remove(img2);
            }
            await dataBaseContext.Images.AddAsync(img);
            await dataBaseContext.SaveChangesAsync();
        }
        public async Task DeleteImageAsync(User user)
        {
            var img = await dataBaseContext.Images.FirstOrDefaultAsync(img => img.UserId == user.Id);
            dataBaseContext.Images.Remove(img);
            await dataBaseContext.SaveChangesAsync();
        }
    }

    public interface IImageDbRepository
    {
        public Task<Image> GetUserImageAsync(User user);
        public Task UpdateUserImageAsync(User user, string Path);
        public Task DeleteImageAsync(User user);
    }
}
