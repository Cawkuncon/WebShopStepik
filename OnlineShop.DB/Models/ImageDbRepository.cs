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
        public Image GetImage(string path)
        {
            return dataBaseContext.Images.FirstOrDefault(img => img.Path == path);
        }
    }

    public interface IImageDbRepository
    {
        public Image GetImage(string path);
    }
}
