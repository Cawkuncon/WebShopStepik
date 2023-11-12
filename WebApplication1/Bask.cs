using WebApplication1.Models;

namespace WebApplication1
{
    public class Bask: IBaskRepository
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

	public interface IBaskRepository
	{
        public void ClearResultProducts();

        public void AddToBask(Product product);

        public List<Product> GetBask();

        public void AddToResultCart(Product prod);

        public List<Product> GetResultCart();
		
	}
}
