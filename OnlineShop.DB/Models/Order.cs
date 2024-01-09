using OnlineShop.DB.Models;
using System.ComponentModel.DataAnnotations;


namespace OnlineShop.DB.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
        public int Total { get; set; }
        public string CreationDate { get; set; }
        public string CreationTime { get; set; }
        public int? Status { get; set; }
        public string? Comments { get; set; }
        public string Address { get; set; }
        public Guid UserId { get; set; }
    }

}
