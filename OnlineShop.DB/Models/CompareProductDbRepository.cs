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

        public void Add(Guid productId, string UserName)
        {
            var newComp = new CompareProduct();
            var product = dataBaseContext.Products.FirstOrDefault(x => x.Id == productId);
            var userId = userManager.FindByNameAsync(UserName).Result.Id;
            newComp.UserId = userId;
            newComp.Product = product;
            dataBaseContext.CompareProducts.Add(newComp);
            dataBaseContext.SaveChanges();
        }

        public List<Product> GetCompareProducts(string UserName)
        {
            if (UserName == null)
            {
                return null;
            }
            var userId = userManager.FindByNameAsync(UserName).Result.Id;
            var prods = dataBaseContext.CompareProducts.Where(prd => prd.UserId == userId).Select(prd=>prd.Product).ToList();
            return prods;
        }

        public void DeleteFromComparsion(Guid productId, string UserName)
        {
            var userId = userManager.FindByNameAsync(UserName).Result.Id;
            var prdComp = dataBaseContext.CompareProducts.Where(prd => prd.UserId == userId && prd.Product.Id == productId).FirstOrDefault();
            dataBaseContext.CompareProducts.Remove(prdComp);
            dataBaseContext.SaveChanges();
        }
    }

    public interface ICompareProductDbRepository
    {
        public void Add(Guid productId, string UserName);
        public List<Product> GetCompareProducts(string UserName);
        public void DeleteFromComparsion(Guid productId, string UserName);
    }
}
