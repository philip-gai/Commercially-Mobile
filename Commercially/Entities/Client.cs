using System;

namespace Commercially
{
	[Serializable]
	public class Client
	{
		public string clientId { get; set; }
		public string friendlyName { get; set; }

		public static string FindFriendlyName(Client[] clients, string clientId)
		{
			foreach (Client client in clients) {
				if (client.clientId.Equals(clientId)) {
					return client.friendlyName;
				}
			}
			return null;
		}
	}
}