using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace OnlineShop.DB.Models
{
    public class OrderDbRepository : IOrderRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public OrderDbRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public async Task AddAsync(Order order)
        {
            await dataBaseContext.Orders.AddAsync(order);
            await dataBaseContext.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllAsync() => await dataBaseContext.Orders.ToListAsync();

        public async Task<Order> GetOrderAsync(Guid id) => await dataBaseContext.Orders.FirstOrDefaultAsync(o => o.Id == id);

        public async Task UpdateStatusAsync(Guid id, int status)
        {
            var ord = await this.GetOrderAsync(id);
            ord.Status = status;
            await dataBaseContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync(Guid id)
        {
            var prods = await dataBaseContext.Carts.Where(cart => cart.OrderId == id).Select(cart => cart.Product).ToListAsync();
            return prods;
        }

        public async Task<List<Order>> GetAllUserOrdersAsync(string id)
        {
            var orders = dataBaseContext.Orders.Where(order => order.User.Id == id);
            return await orders.ToListAsync();
        }
    } 

    public interface IOrderRepository
    {

        public Task<List<Order>> GetAllAsync();
        public Task AddAsync(Order order);
        public Task<Order> GetOrderAsync(Guid id);
        public Task UpdateStatusAsync(Guid id, int status);
        public Task<List<Product>> GetAllProductsAsync(Guid id);
        public Task<List<Order>> GetAllUserOrdersAsync(string id);

    }
}
