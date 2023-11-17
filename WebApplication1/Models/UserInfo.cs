
namespace WebApplication1.Models
{
	public class UserInfo : IUserInfo
	{
		public string Name { get ; set; }
		public string Password { get ; set; }
		public string CheckBox { get; set; }
		public bool SaveUser { get 
			{ 
				if (CheckBox == "on")
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
		string CheckBox { get; set; }
	}
}
