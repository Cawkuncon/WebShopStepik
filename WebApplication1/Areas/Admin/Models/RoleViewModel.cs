using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.Admin.Models
{
    public class RoleViewModel : IRole
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Заполните имя роли")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Имя роли должно быть от 2 до 20 символов")]
        public string Name { get; set; }
    }

    public interface IRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
