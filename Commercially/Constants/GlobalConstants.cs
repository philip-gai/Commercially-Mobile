// Create by Philip Gai

namespace Commercially
{
	/// <summary>
	/// The global constants used in the application.
	/// </summary>
	public struct GlobalConstants
	{
		/// <summary>
		/// The server URL.
		/// </summary>
		public const string ServerUrl = "192.168.3.126";

		/// <summary>
		/// The server port.
		/// </summary>
		public const string ServerPort = "8443";

		/// <summary>
		/// Screen names.
		/// </summary>
		public struct Screens
		{
			public const string Home = "Home";
			public const string Login = "Login";
			public const string Prompt = "Prompt";
			public const string RequestDetails = "RequestDetails";
			public const string RequestList = "RequestList";
			public const string ButtonDetails = "ButtonDetails";
			public const string UserDetails = "UserDetails";
			public const string ClientDetails = "ClientDetails";
			public const string UserCreate = "UserCreate";
		}

		/// <summary>
		/// Default colors used in the application.
		/// </summary>
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
			public static Color Black = new Color(0, 0, 0);
			public static Color White = new Color(255, 255, 255);
		}

		/// <summary>
		/// Strings used internally.
		/// </summary>
		public struct Strings
		{
			public const string Ignore = "ignore";
		}
	}
}
