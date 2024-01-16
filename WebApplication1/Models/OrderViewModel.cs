using OnlineShop.DB.Models;
using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{

    public enum Status_Order
    {
        Created,
        Processed,
        Delivering,
        Canceled,
        Delivered,
    }

    public class OrderViewModel: IOrder
    {
        public Guid Id { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public int? Status { get; set; }

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

        public string CreationDate { get; set; }
        public string CreationTime { get; set; }
        public Status_Order? StatusOrder
        {
            get
            {
                return (Status_Order)this.Status;
            }
            set
            {
                this.Status = (int)value;
            }
        }
        public string? Comments { get; set; }

        [Required(ErrorMessage = "Не указан адрес доставки")]
        public string Address { get; set; }

        public void OrderCreation()
        {
            CreationDate = DateTime.Now.ToShortDateString();
            CreationTime = DateTime.Now.ToShortTimeString();
            Status = (int)Status_Order.Created;
        }
    }


    public interface IOrder
    {
        public Guid Id { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public int Total { get; set; }
        public string CreationDate { get; set; }
        public string CreationTime { get; set; }
        public int? Status { get; set; }
        public string? Comments { get; set; }
        public string Address { get; set; }
    }
}
