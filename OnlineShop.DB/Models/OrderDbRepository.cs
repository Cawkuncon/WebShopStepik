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


        public void Add(Order order)
        {
            dataBaseContext.Orders.Add(order);
            dataBaseContext.SaveChanges();
        }

        public void AddProduct(Order order, Product product)
        {
            order.Products.Add(product);
            dataBaseContext.SaveChanges();
        }

        public List<Order> GetAll()
        {
            return dataBaseContext.Orders.ToList();
        }
    }

    public interface IOrderRepository
    {

        public List<Order> GetAll();
        public void Add(Order order);
        public void AddProduct(Order order, Product product);

    }
}
