using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Не указано имя товара")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Имя товара должно содержать от 2 до 30 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана цена товара")]
        [Range(1, 1000000, ErrorMessage = "Укажите цену товара в диапазоне от 1 до 1000000")]
        public int Cost { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public bool Compare { get; set; } = false;
        public bool Favorite { get; set; } = false;


        public ProductViewModel()
        {

        }

        public ProductViewModel(Guid id, int cost, string name, string descr, int count)
        {
            Id = id;
            Cost = cost;
            Name = name;
            Count = count;
        }
    }
}
