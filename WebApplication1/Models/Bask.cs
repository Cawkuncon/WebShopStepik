using WebApplication1.Models;

namespace WebApplication1
{
	public class Bask : IBaskRepository
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

		public void AddToCart(Product product)
		{
			prodCart.Add(product);
		}

		public List<Product> GetCart()
		{
			return prodCart;
		}

		public void AddToResultProducts(Product prod)
		{
			resultProducts.Add(prod);
		}

		public List<Product> GetResultProducts()
		{
			return resultProducts;
		}

		public void ClearCart()
		{
			prodCart.Clear();
		}

		public void RemoveFromCart(int Id)
		{
			var prod = prodCart.Where(x => x.Id == Id).First();
			prodCart.Remove(prod);
		}
	}

	public interface IBaskRepository
	{
		public void ClearResultProducts();

		public void ClearCart();

		public void AddToCart(Product product);
		public List<Product> GetCart();
		public void AddToResultProducts(Product prod);
		public void RemoveFromCart(int Id);
		public List<Product> GetResultProducts();

	}
}
