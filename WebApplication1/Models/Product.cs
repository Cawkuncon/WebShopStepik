using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указано имя товара")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Имя товара должно содержать от 2 до 30 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана цена товара")]
        [Range(1, 1000000, ErrorMessage = "Укажите цену товара в диапазоне от 1 до 1000000")]
        public int Cost { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }

        public bool Comparsion = false;

        public bool Favorite = false;
        public Product()
        {

        }
        public Product(int cost, string name, string description)
        {
            Name = name;
            Cost = cost;
            Description = description;
        }
        public Product(int id, int cost, string name, string description)
        {
            Id = id;
            Name = name;
            Cost = cost;
            Description = description;
            Count = 1;
        }

        public Product(int id, int cost, string name, string description, int count)
        {
            Id = id;
            Name = name;
            Cost = cost;
            Description = description;
            Count = count;
        }

        public override string ToString()
        {
            return $"{Id}\n{Name}\n{Cost}\n{Description}\n\n";
        }

    }
}
