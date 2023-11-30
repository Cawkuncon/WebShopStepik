using WebApplication1.Models;

namespace WebApplication1
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var services = builder.Services;
			services.AddSingleton<ICounter, RandomCounter2>();
			services.AddSingleton<CounterService>();
			services.AddSingleton<IBaskRepository,Bask>();
			services.AddSingleton<IProductRepository,ProductRepository>();
			services.AddSingleton<IOrderRepository,OrderRepository>();
			services.AddTransient<IOrder, Order>();
			// Add services to the container.
			builder.Services.AddControllersWithViews();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();


			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}");
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Product}/{action=Index}/{id}");
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Basket}/{action=Index}/");

			app.Run();
		}
	}
}