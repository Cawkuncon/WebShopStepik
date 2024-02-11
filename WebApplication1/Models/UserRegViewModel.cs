using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Areas.Admin.Models;

namespace WebApplication1.Models
{
    public class UserRegViewModel : IUserReg
    {
        public string? UserId { get; set; }
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Имя должно содержать от 3 до 20 символов")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Не указан пароль")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "Пароль должен содержать от 8 символов. При этом обязательно в пароле должна быть хотя бы одна цифра, одна буква в нижнем регистре и одна буква в верхнем регистре.")]

        public string Password { get; set; }


        [Required(ErrorMessage = "Не указан пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string Password2 { get; set; }


        [Required(ErrorMessage = "Не указан возраст")]
        [Range(18, 90, ErrorMessage = "Возраст от 18 до 90 лет")]
        public int Age { get; set; }


        [Required(ErrorMessage = "Не указан адрес")]
        [EmailAddress(ErrorMessage = "Введите валидный email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Не указан телефон")]
        [RegularExpression(@"^\d{1}-\d{3}-\d{3}-\d{2}-\d{2}", ErrorMessage = "Неверный формат номера. Введите номер в формате 8-888-888-88-88")]
        public string Number { get; set; }

        public RoleViewModel? Role { get; set; }

        public bool PassCheck()
        {
            return Password == Password2 && Password != null;
        }
        public string? ReturnUrl { get; set; }
    }

    public interface IUserReg
    {
        string Name { get; set; }
        string Password { get; set; }
        string Password2 { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string ReturnUrl { get; set; }
    }
}