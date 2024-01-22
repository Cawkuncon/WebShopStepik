using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DB.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public string? Description { get; set; }
        public bool Favorite { get; set; }
        public bool Comparsion { get; set; }    


    }
}
