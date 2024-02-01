﻿using Microsoft.EntityFrameworkCore;
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
        public CompareProductDbRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public void Add(Guid productId, Guid userId)
        {
            var newComp = new CompareProduct();
            var product = dataBaseContext.Products.FirstOrDefault(x => x.Id == productId);
            newComp.UserId = userId;
            newComp.Product = product;
            dataBaseContext.CompareProducts.Add(newComp);
            dataBaseContext.SaveChanges();
        }

        public List<Product> GetCompareProducts(Guid userId)
        {
            var prods = dataBaseContext.CompareProducts.Where(prd => prd.UserId == userId).Select(prd=>prd.Product).ToList();
            return prods;
        }

        public void DeleteFromComparsion(Guid productId, Guid userId)
        {
            var prdComp = dataBaseContext.CompareProducts.Where(prd => prd.UserId == userId && prd.Product.Id == productId).FirstOrDefault();
            dataBaseContext.CompareProducts.Remove(prdComp);
            dataBaseContext.SaveChanges();
        }
    }

    public interface ICompareProductDbRepository
    {
        public void Add(Guid productId, Guid userId);
        public List<Product> GetCompareProducts(Guid userId);
        public void DeleteFromComparsion(Guid productId, Guid userId);
    }
}
