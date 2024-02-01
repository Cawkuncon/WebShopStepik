using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DB.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<UserReg>? Users { get; set; }
    }
}
