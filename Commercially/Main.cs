using System.Net;
namespace Commercially
{
	public static class Main
	{
		public static void Initialize() {
			ServicePointManager.ServerCertificateValidationCallback = delegate {
				return true;
			};
		}
	}
}
