using WebApplication1.Models;

namespace WebApplication1
{
	public class ProductRepository : IProductRepository
	{

		public List<Product> listProducts = new List<Product>()
		{
			new Product(1, 11, "Name1", "Descr1dfdfdfdfdfdfdfdfdf dfdfdfdfdfdfdfdfdfdfdf dfdfdfdfdfdfdfdfdfdfdf dfdfdfdfdfdfdfd fdfdfdfdfdfdfdfdfdfdfdfd dfdfdfdfdfdfdfdf dfdfdfdfdfdfdfdf"),
			new Product(2, 22, "Name2", "Descr2dddddddddddddd"),
			new Product(3, 33, "Name3", "Descr3sssssssee eeeeeeeee eeeeeeeeeeee eeeeeeeeeeeeeeeee eeeeeeeeeeee eeeeeeeeee eeeeeeeeeeeeee"),
			new Product(4, 44, "Name4", "Descr4ddddddd"),
			new Product(5, 55, "Name5", "Descr5eee"),
			new Product(6, 66, "Name6", "Descr6e eeeee eeeeee eeeeeeeeee eeeeeeeeeeeeeeee eeeeeeeeeeee eeeeeeeee eee eeeeeeeee eeeeee eee"),
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
