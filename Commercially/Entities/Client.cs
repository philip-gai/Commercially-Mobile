using System;

namespace Commercially
{
	[Serializable]
	public class Client
	{
		public string clientId { get; set; }
		public string friendlyName { get; set; }
		public bool authorized { get; set; }

		public static Client FindClient(string clientInfo)
		{
			return FindClients(new string[] { clientInfo })[0];
		}

		public static Client[] FindClients(string[] clientsInfo)
		{
			var clients = ClientApi.GetClients();
			var matchedClients = new Client[clientsInfo.Length];
			for (int i = 0; i < clientsInfo.Length; i++) {
				var clientInfo = clientsInfo[i];
				matchedClients[i] = Array.Find(clients, (Client client) => {
					return client.clientId.Equals(clientInfo, StringComparison.CurrentCultureIgnoreCase)
								 || (!string.IsNullOrWhiteSpace(client.friendlyName) && client.friendlyName.Equals(clientInfo, StringComparison.CurrentCultureIgnoreCase));
				});
			}
			return matchedClients;
		}
	}
}