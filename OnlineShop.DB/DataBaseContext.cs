using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB
{
    public class DataBaseContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserReg> UserRegs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CartItem> Carts { get; set; }
        public DbSet<CompareProduct> CompareProducts { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options): base(options) 
        { 
            Database.EnsureCreated();
        }
    }
}
