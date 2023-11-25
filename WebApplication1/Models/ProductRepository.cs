using WebApplication1.Models;

namespace WebApplication1
{
	public class ProductRepository : IProductRepository
	{

		public List<Product> listProducts = new List<Product>()
		{
			new Product(1, 11, "Name1", "Descr1"),
			new Product(2, 22, "Name2", "Descr2"),
			new Product(3, 33, "Name3", "Descr3"),
			new Product(4, 44, "Name4", "Descr4"),
			new Product(5, 55, "Name5", "Descr5"),
			new Product(6, 66, "Name6", "Descr6"),
		};

		public List<Product> GetAll()
		{
			return listProducts;
		}
		public void Delete(int id)
		{
			var prod = listProducts.Find(x => x.Id == id);
			listProducts.Remove(prod);
		}

        public void AddProd(Product prod)
        {
            listProducts.Add(prod);
        }

        public void UpdateProd(Product prod)
        {
			var product = listProducts.Find(x => x.Id == prod.Id);
			product.Name = prod.Name;
			product.Description = prod.Description;
			product.Cost = prod.Cost;
        }
    }

	public interface IProductRepository
	{
		public List<Product> GetAll();
		public void Delete(int id);
		public void AddProd(Product prod);
		public void UpdateProd(Product prod);
	}
}
