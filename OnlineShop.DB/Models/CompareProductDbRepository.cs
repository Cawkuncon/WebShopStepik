using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB.Models
{
    public class CompareProductDbRepository : ICompareProductDbRepository
    {
        private readonly DataBaseContext dataBaseContext;
        private readonly UserManager<User> userManager;
        public CompareProductDbRepository(DataBaseContext dataBaseContext, UserManager<User> userManager)
        {
            this.dataBaseContext = dataBaseContext;
            this.userManager = userManager;
        }

        public async Task AddAsync(Guid productId, string UserName)
        {
            var newComp = new CompareProduct();
            var product = await dataBaseContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
            var userId = userManager.FindByNameAsync(UserName).Result.Id;
            newComp.UserId = userId;
            newComp.Product = product;
            await dataBaseContext.CompareProducts.AddAsync(newComp);
            await dataBaseContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetCompareProductsAsync(string UserName)
        {
            if (UserName == null)
            {
                return null;
            }
            var userId = userManager.FindByNameAsync(UserName).Result.Id;
            var prods = await dataBaseContext.CompareProducts.Where(prd => prd.UserId == userId).Select(prd=>prd.Product).ToListAsync();
            return prods;
        }

        public async Task DeleteFromComparsionAsync(Guid productId, string UserName)
        {
            var userId = userManager.FindByNameAsync(UserName).Result.Id;
            var prdComp = await dataBaseContext.CompareProducts.Where(prd => prd.UserId == userId && prd.Product.Id == productId).FirstOrDefaultAsync();
            dataBaseContext.CompareProducts.Remove(prdComp);
            await dataBaseContext.SaveChangesAsync();
        }
    }

    public interface ICompareProductDbRepository
    {
        public Task AddAsync(Guid productId, string UserName);
        public Task<List<Product>> GetCompareProductsAsync(string UserName);
        public Task DeleteFromComparsionAsync(Guid productId, string UserName);
    }
}
