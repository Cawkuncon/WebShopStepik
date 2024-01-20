using OnlineShop.DB.Models;
using System.ComponentModel.DataAnnotations;


namespace OnlineShop.DB.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public int Total { get; set; }
        public string CreationDate { get; set; }
        public string CreationTime { get; set; }
        public int Status { get; set; }
        public string? Comments { get; set; }
        public string Address { get; set; }
        public UserReg? User { get; set; }
    }
}
