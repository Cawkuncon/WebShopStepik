using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Helpers
{
    public class UserAuthSession: IUserAuth
    {
        public Guid UserId { get; set; }
        public static bool Auth { get; set; } = false;
    }

    public interface IUserAuth
    {
        public Guid UserId { get; set; }
        public static bool Auth { get; set; }
    }
}
