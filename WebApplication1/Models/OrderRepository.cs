namespace WebApplication1.Models
{
    public class OrderRepository : IOrderRepository
    {
        private List<Order> _orders = new List<Order>()
        {

        };

        public void Add(Order order)
        {
            _orders.Add(order);
        }

        public List<Order> GetAll()
        {
            return _orders;
        }
    }

    public interface IOrderRepository
    {

        public List<Order> GetAll();
        public void Add(Order order);

    }
}
