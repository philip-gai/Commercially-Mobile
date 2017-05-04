// Created by Philip Gai

using System.Net;
namespace Commercially
{
	/// <summary>
	/// Main manager.
	/// </summary>
	public static class MainManager
	{
		/// <summary>
		/// Initialize this instance.
		/// </summary>
		public static void Initialize()
		{
			ServicePointManager.ServerCertificateValidationCallback = delegate {
				return true;
			};
		}
	}
}
