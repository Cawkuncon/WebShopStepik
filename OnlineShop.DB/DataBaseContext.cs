using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace OnlineShop.DB
{
    public class DataBaseContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options): base(options) 
        { 
            Database.EnsureCreated();
        }
    }
}
