using WebApplication1.Models;

namespace WebApplication1
{
    public class Bask
    {
        private List<Product> prodCart = new List<Product>()
        {

        };

        private List<Product> resultProducts = new List<Product>()
        {

        };

        public void ClearResultProducts()
        {
            resultProducts.Clear();
        }

        public void AddToBask(Product product)
        {
            prodCart.Add(product);
        }

        public List<Product> GetBask()
        {
            return prodCart;
        }

        public void AddToResultCart(Product prod)
        {
            resultProducts.Add(prod);
        }

        public List<Product> GetResultCart()
        {
            return resultProducts;
        }
    }
}
