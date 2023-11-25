
namespace WebApplication1.Models
{
	public class UserInfo : IUserInfo
	{
		public string Name { get ; set; }
		public string Password { get ; set; }
		public string SaveUser { get; set; }
		public bool SaveUserInfo { get 
			{ 
				if (SaveUser == "on")
				{
					return true;
				}
				else
				{
					return false;
				}
			} 
		}
	}

	public interface IUserInfo
	{
		string Name { get; set; }
		string Password { get; set; }
		string SaveUser { get; set; }
	}
}
