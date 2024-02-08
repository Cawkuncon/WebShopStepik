using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DB.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public string? Description { get; set; }
        public List<Image>? Images { get; set; }
        public Product()
        {

        }
        public Product(string name, int cost, string description)
        {
            Id = Guid.NewGuid();
            Name= name;
            Cost= cost;
            Description= description;
        }
    }
}
