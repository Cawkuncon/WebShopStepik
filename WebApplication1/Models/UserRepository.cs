namespace WebApplication1.Models
{
    public class UserRepository : IUserRepository
    {
        private List<UserReg> Users = new List<UserReg>();

        public void Add(UserReg user)
        {
            Users.Add(user);
        }

        public void DeleteUser(UserReg user)
        {
            Users.Remove(user);
        }

        public UserReg GetUser(string Name)
        {
            var user = Users.FirstOrDefault(x => x.Name == Name);
            return user;
        }

        public List<UserReg> GetUsers()
        {
            return Users;
        }
    }

    public interface IUserRepository
    {
        public void Add(UserReg user);
        public UserReg GetUser(string Name);
        public List<UserReg> GetUsers();
        public void DeleteUser(UserReg user);
    }
}
