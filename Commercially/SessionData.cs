using System.Threading.Tasks;

namespace Commercially
{
	public static class SessionData
	{
		public static User User;
		public static OAuthResponse OAuth;
		public static Request[] Requests;
		public static FlicButton[] Buttons;
		public static Client[] Clients;
		public static TaskFactory TaskFactory = new TaskFactory();
		public static bool TestMode;
	}
}
