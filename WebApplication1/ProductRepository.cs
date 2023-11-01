using WebApplication1.Models;

namespace WebApplication1
{
    public class ProductRepository
    {
        private List<Product> listProducts = new List<Product>()
        {
            new Product(1, 11, "Name1", "Descr1"),
            new Product(2, 22, "Name2", "Descr2"),
            new Product(3, 33, "Name3", "Descr3"),
        };

        public List<Product> GetAll()
        {
            return listProducts;
        }
    }
}
