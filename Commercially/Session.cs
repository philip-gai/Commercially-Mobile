using System.Threading.Tasks;

namespace Commercially
{
	public static class Session
	{
		public static User User;
		public static OAuthResponse OAuth;
		public static TaskFactory TaskFactory = new TaskFactory();
	}
}
