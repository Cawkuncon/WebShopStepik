using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public Product? Product { get; set; }
        public string? UserId { get; set; }

    }
}
