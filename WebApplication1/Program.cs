using WebApplication1.Models;
using Serilog;
using WebApplication1.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using WebApplication1.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            services.AddSingleton<IBaskRepository, Bask>();
            services.AddTransient<IProductRepository, ProductDbRepository>();
            services.AddTransient<IOrderRepository, OrderDbRepository>();
            services.AddTransient<IOrder, OrderViewModel>();
            services.AddTransient<ICartItemDbRepository, CartItemDbRepository>();
            services.AddTransient<ICompareProductDbRepository, CompareProductDbRepository>();
            services.AddTransient<IFavoriteProductDbRepository, FavoriteProductDbRepository>();
            services.AddTransient<IImageDbRepository, ImageDbRepository>();

            string connection = builder.Configuration.GetConnectionString("onlineShop");
            builder.Services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connection));
            builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connection));
            builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
            builder.Host.UseSerilog((context, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.WithProperty("ApplicationName", "WebApplication1"));
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(0);
                options.LoginPath = "/Login/Login";
                options.LogoutPath = "/Login/Logout";
                options.Cookie = new CookieBuilder
                {
                    IsEssential= true,
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            using (var serviceScope = app.Services.CreateScope())
            {
                var servicess = serviceScope.ServiceProvider;
                var userManager = servicess.GetRequiredService<UserManager<User>>();
                var rolesManager = servicess.GetRequiredService<RoleManager<IdentityRole>>();
                IdentityInitializer.Initialize(userManager, rolesManager);
            }

            app.MapControllerRoute(
                name: "MyArea",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}