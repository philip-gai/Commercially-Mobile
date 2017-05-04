using UIKit;

namespace Commercially.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main(string[] args)
		{
			MainManager.Initialize();
			UIApplication.Main(args, null, "AppDelegate");
		}
	}
}
