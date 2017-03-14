using System.Net;
using UIKit;

namespace Commercially.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main(string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			ServicePointManager.ServerCertificateValidationCallback = delegate {
				return true;
			};
			UIApplication.Main(args, null, "AppDelegate");
		}
	}
}
