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

        public void Add(Guid orderId, Guid productId)
        {
            var cartItem = new CartItem();
            cartItem.OrderId = orderId;
            cartItem.Product = dataBaseContext.Products.FirstOrDefault(x => x.Id == productId);
            if (cartItem.Product != null)
            {
                dataBaseContext.Carts.Add(cartItem);
                dataBaseContext.SaveChanges();
            }
        }

        public CartItem GetCartItem(int CartId)
        {
            return dataBaseContext.Carts.FirstOrDefault(x=>x.Id == CartId);
        }

        public List<CartItem> GetOrdersCarts(Guid orderId)
        {
            return dataBaseContext.Carts.Where(cart => cart.OrderId == orderId).ToList();
        }
    }

    public interface ICartItemDbRepository
    {
        public void Add(Guid orderId, Guid productId);
        public CartItem GetCartItem(int CartId);
        public List<CartItem> GetOrdersCarts(Guid orderId);
    }
}
