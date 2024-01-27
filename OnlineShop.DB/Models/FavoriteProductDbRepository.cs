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

        public FavoriteProductDbRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }
        public void AddProdToFavor(Guid productId, Guid userId)
        {
            var newFavor = new FavoriteProduct();
            var product = dataBaseContext.Products.FirstOrDefault(x => x.Id == productId);
            newFavor.UserId = userId;
            newFavor.Product = product;
            dataBaseContext.FavoriteProducts.Add(newFavor);
            dataBaseContext.SaveChanges();
        }
        public List<Product> GetFavoriteProducts(Guid userId)
        {
            var prods = dataBaseContext.FavoriteProducts.Where(prd => prd.UserId == userId).Select(prd => prd.Product).ToList();
            return prods;
        }

        public void DeleteFromFavorite(Guid productId, Guid userId)
        {
            var prdFav = dataBaseContext.FavoriteProducts.Where(prd => prd.UserId == userId && prd.Product.Id == productId).FirstOrDefault();
            dataBaseContext.FavoriteProducts.Remove(prdFav);
            dataBaseContext.SaveChanges();
        }
    }

    public interface IFavoriteProductDbRepository
    {
        public void AddProdToFavor(Guid productId, Guid userId);
        public List<Product> GetFavoriteProducts(Guid userId);
        public void DeleteFromFavorite(Guid productId, Guid userId);

    }
}
