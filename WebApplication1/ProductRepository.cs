using WebApplication1.Models;

namespace WebApplication1
{
    public class ProductRepository
    {
        public static List<Product> prodCart = new List<Product>()
        {

        };
        private static List<Product> listProducts = new List<Product>()
        {
            new Product(1, 11, "Name1", "Descr1"),
            new Product(2, 22, "Name2", "Descr2"),
            new Product(3, 33, "Name3", "Descr3"),
            new Product(4, 44, "Name4", "Descr4"),
            new Product(5, 55, "Name5", "Descr5"),
            new Product(6, 66, "Name6", "Descr6"),
        };

        public static List<Product> GetAll()
        {
            return listProducts;
        }
    }
}
