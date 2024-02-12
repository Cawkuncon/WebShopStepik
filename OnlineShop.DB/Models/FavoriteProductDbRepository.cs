using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB.Models
{
    public class FavoriteProductDbRepository : IFavoriteProductDbRepository
    {
        private readonly DataBaseContext dataBaseContext;
        private readonly UserManager<User> userManager;

        public FavoriteProductDbRepository(DataBaseContext dataBaseContext, UserManager<User> userManager)
        {
            this.dataBaseContext = dataBaseContext;
            this.userManager = userManager;
        }
        public void AddProdToFavor(Guid productId, string UserName)
        {
            var newFavor = new FavoriteProduct();
            var product = dataBaseContext.Products.FirstOrDefault(x => x.Id == productId);
            newFavor.UserId = userManager.FindByNameAsync(UserName).Result.Id;
            newFavor.Product = product;
            dataBaseContext.FavoriteProducts.Add(newFavor);
            dataBaseContext.SaveChanges();
        }
        public List<Product> GetFavoriteProducts(string UserName)
        {
            if (UserName == null)
            {
                return null;
            }
            var userId = userManager.FindByNameAsync(UserName).Result.Id;
            var prods = dataBaseContext.FavoriteProducts.Where(prd => prd.UserId == userId).Select(prd => prd.Product).ToList();
            return prods;
        }

        public void DeleteFromFavorite(Guid productId, string UserName)
        {
            var userId = userManager.FindByNameAsync(UserName).Result.Id;
            var prdFav = dataBaseContext.FavoriteProducts.Where(prd => prd.UserId == userId && prd.Product.Id == productId).FirstOrDefault();
            dataBaseContext.FavoriteProducts.Remove(prdFav);
            dataBaseContext.SaveChanges();
        }
    }

    public interface IFavoriteProductDbRepository
    {
        public void AddProdToFavor(Guid productId, string UserName);
        public List<Product> GetFavoriteProducts(string UserName);
        public void DeleteFromFavorite(Guid productId, string UserName);

    }
}
