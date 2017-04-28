namespace Commercially
{
	public class ClientTableRow
	{
		public readonly Client Client;

		public ClientTableRow(Client client)
		{
			Client = client;
		}

		public string IdText {
			get {
				return Client.clientId;
			}
		}

		public string FriendlyNameText {
			get {
				return Client.friendlyName;
			}
		}

		public bool FriendlyNameLabelIsHidden {
			get {
				return string.IsNullOrWhiteSpace(Client.friendlyName);
			}
		}
	}
}
