using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        public async Task AddProdToFavorAsync(Guid productId, string UserName)
        {
            var newFavor = new FavoriteProduct();
            var product = await dataBaseContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
            newFavor.UserId = userManager.FindByNameAsync(UserName).Result.Id;
            newFavor.Product = product;
            await dataBaseContext.FavoriteProducts.AddAsync(newFavor);
            await dataBaseContext.SaveChangesAsync();
        }
        public async Task<List<Product>> GetFavoriteProductsAsync(string UserName)
        {
            if (UserName == null)
            {
                return null;
            }
            var userId = userManager.FindByNameAsync(UserName).Result.Id;
            var prods = await dataBaseContext.FavoriteProducts.Where(prd => prd.UserId == userId).Select(prd => prd.Product).ToListAsync();
            return prods;
        }

        public async Task DeleteFromFavoriteAsync(Guid productId, string UserName)
        {
            var userId = userManager.FindByNameAsync(UserName).Result.Id;
            var prdFav = await dataBaseContext.FavoriteProducts.Where(prd => prd.UserId == userId && prd.Product.Id == productId).FirstOrDefaultAsync();
            dataBaseContext.FavoriteProducts.Remove(prdFav);
            await dataBaseContext.SaveChangesAsync();
        }
    }

    public interface IFavoriteProductDbRepository
    {
        public Task AddProdToFavorAsync(Guid productId, string UserName);
        public Task<List<Product>> GetFavoriteProductsAsync(string UserName);
        public Task DeleteFromFavoriteAsync(Guid productId, string UserName);

    }
}
