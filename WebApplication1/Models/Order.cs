namespace WebApplication1.Models
{
	public class Order: IOrder
	{
		public List<Product> Products { get; set; }
		public string? Name { get; set; }
		public string? Number { get; set; }
		public string? Email { get; set; }
		public int Total { get; set; }

		public void SaveOrder()
		{
			
		}
	}

	public interface IOrder
	{
		public List<Product> Products { get; set; }
		public string? Name { get; set; }
		public string? Number { get; set; }
		public string? Email { get; set; }
		public int Total { get; set; }

		public void SaveOrder();

	}
}
