using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB.Models
{
    public class UserReg
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Password2 { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public string Number { get; set; }

        public List<Order> Order { get; set; }

        public Role Role { get; set; }


    }
}
