using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> Carts { get; set; }
        public DbSet<CompareProduct> CompareProducts { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new List<Product>()
            {
                new Product(name:"First", cost: 11, description:"First Product"),
                new Product(name:"Second", cost: 22, description:"Second Product"),
                new Product(name:"Third", cost: 3, description:"Third Product"),
            });
        }
    }
}
