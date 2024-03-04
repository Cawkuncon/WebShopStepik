using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB.Models
{
    public class CartItemDbRepository: ICartItemDbRepository
    {
        private readonly DataBaseContext dataBaseContext;
        public CartItemDbRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public async Task AddAsync(Guid orderId, Guid productId)
        {
            var cartItem = new CartItem();
            cartItem.OrderId = orderId;
            cartItem.Product = await dataBaseContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (cartItem.Product != null)
            {
                await dataBaseContext.Carts.AddAsync(cartItem);
                await dataBaseContext.SaveChangesAsync();
            }
        }

        public async Task<CartItem> GetCartItemAsync(int CartId) => await dataBaseContext.Carts.FirstOrDefaultAsync(x => x.Id == CartId);

        public async Task<List<CartItem>> GetOrdersCartsAsync(Guid orderId) => await dataBaseContext.Carts.Where(cart => cart.OrderId == orderId).ToListAsync();
    }

    public interface ICartItemDbRepository
    {
        public Task AddAsync(Guid orderId, Guid productId);
        public Task<CartItem> GetCartItemAsync(int CartId);
        public Task<List<CartItem>> GetOrdersCartsAsync(Guid orderId);
    }
}
