using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB.Models
{
    public class CompareProduct
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }    
        public Product Product { get; set; }
    }
}
