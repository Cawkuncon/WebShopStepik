using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.DB.Models
{
    public class UserRegDbRepository: IUserRegDbRepository
    {
        private readonly DataBaseContext dataBaseContext;
        public UserRegDbRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public List<UserReg> GetAll()
        {
            return dataBaseContext.UserRegs.ToList();
        }

        public UserReg Get(Guid id) 
        {
            return dataBaseContext.UserRegs.FirstOrDefault(x=> x.UserId == id);
        }

        public void Delete(Guid id)
        {
            var user = dataBaseContext.UserRegs.FirstOrDefault(y=> y.UserId == id);
            if (user != null)
            {
                dataBaseContext.UserRegs.Remove(user);
                dataBaseContext.SaveChanges();
            }
        }
        public void Add (UserReg userReg)
        {
            dataBaseContext.UserRegs.Add(userReg);
            dataBaseContext.SaveChanges();
        }
    }

    public interface IUserRegDbRepository
    {
        public List<UserReg> GetAll();
        public UserReg Get(Guid id);
        public void Delete(Guid id);
        public void Add(UserReg userReg);
    }
}
