using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{
	public class UserInfo : IUserInfo
	{
		[Required(ErrorMessage = "Не указан логин")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Имя должно содержать от 3 до 20 символов")]
        public string Name { get ; set; }


        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get ; set; }
		public string SaveUser { get; set; }
		public bool SaveUserInfo { get 
			{ 
				if (SaveUser == "on")
				{
					return true;
				}
				return false;
			} 
		}
		public string ReturnUrl { get; set; }
	}

	public interface IUserInfo
	{
		string Name { get; set; }
		string Password { get; set; }
		string SaveUser { get; set; }
		string ReturnUrl { get; set; }
	}
}
