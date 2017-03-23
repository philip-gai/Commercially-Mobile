namespace Commercially
{
	public struct GlobalConstants
	{
		public const string ServerUrl = "192.168.3.126";
		public const string ServerPort = "8443";
		public struct Screens
		{
			public const string Home = "Home";
			public const string Login = "Login";
			public const string Prompt = "Prompt";
			public const string RequestDetails = "RequestDetails";
		}
		public struct DefaultColors
		{
			public static Color Red = new Color(255, 59, 48);
			public static Color Orange = new Color(255, 149, 0);
			public static Color Yellow = new Color(255, 204, 0);
			public static Color Green = new Color(76, 217, 100);
			public static Color TealBlue = new Color(90, 200, 250);
			public static Color Blue = new Color(0, 122, 255);
			public static Color Purple = new Color(88, 86, 214);
			public static Color Pink = new Color(255, 45, 85);
		}
	}
}
