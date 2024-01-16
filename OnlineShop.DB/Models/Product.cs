using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DB.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public string? Description { get; set; }
        public int Count { get; set; }

        public bool Comparsion = false;

        public bool Favorite = false;
        public List<Order> Ords { get; set; } = new List<Order>();
    }
}
