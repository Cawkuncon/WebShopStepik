namespace WebApplication1.Models
{
    public class UserReg : IUserReg
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string Password2 { get; set; }

        public bool PassCheck()
        {
            return Password == Password2 && Password != null;
        }
    }

    public interface IUserReg
    {
        string Name { get; set; }
        string Password { get; set; }
        string Password2 { get; set; }
    }
}