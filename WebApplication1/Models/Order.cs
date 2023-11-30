using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
	public class Order: IOrder
	{
		public List<Product> Products { get; set; }


        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Имя должно содержать от 3 до 20 символов")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Не указан телефон")]
        [RegularExpression(@"^\d{1}-\d{3}-\d{3}-\d{2}-\d{2}", ErrorMessage = "Неверный формат номера. Введите номер в формате 8-888-888-88-88")]
        public string Number { get; set; }


        [Required(ErrorMessage = "Не указан адрес")]
        [EmailAddress(ErrorMessage = "Введите валидный email")]
        public string Email { get; set; }
		public int Total { get; set; }

		public void SaveOrder()
		{
			
		}
	}

	public interface IOrder
	{
		public List<Product> Products { get; set; }
		public string Name { get; set; }
		public string Number { get; set; }
		public string Email { get; set; }
		public int Total { get; set; }

		public void SaveOrder();

	}
}
