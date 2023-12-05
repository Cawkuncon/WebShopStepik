using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Role: IRole
    {
        private static int PubId = 1;
        public int Id { get; set; }

        [Required(ErrorMessage = "Заполните имя роли")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Имя роли должно быть от 2 до 20 символов")]
        public string Name { get; set; }
        public Role()
        {

        }
        public Role(string name)
        {
            Id = PubId;
            Name = name;
            PubId++;
        }
    }

    public interface IRole
    {
        private static int PubId;
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
