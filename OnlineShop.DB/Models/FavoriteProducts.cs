using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB.Models
{
    public class FavoriteProducts
    {
        [Key]
        public int Id;
        public Product Product { get; set; }
        
    }
}
