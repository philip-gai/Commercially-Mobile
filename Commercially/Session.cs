// Created by Philip Gai

using System.Threading.Tasks;

namespace Commercially
{
	/// <summary>
	/// Session.
	/// </summary>
	public static class Session
	{
		/// <summary>
		/// The user.
		/// </summary>
		public static User User;

		/// <summary>
		/// The OA uth.
		/// </summary>
		public static OAuthResponse OAuth;

		/// <summary>
		/// The task factory.
		/// </summary>
		public static TaskFactory TaskFactory = new TaskFactory();
	}
}
