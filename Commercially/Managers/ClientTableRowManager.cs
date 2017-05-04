// Created by Philip Gai

namespace Commercially
{
	/// <summary>
	/// Client table row manager.
	/// </summary>
	public class ClientTableRowManager
	{
		/// <summary>
		/// The client.
		/// </summary>
		public readonly Client Client;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Commercially.ClientTableRowManager"/> class.
		/// </summary>
		/// <param name="client">Client.</param>
		public ClientTableRowManager(Client client)
		{
			Client = client;
		}

		/// <summary>
		/// Gets the identifier text.
		/// </summary>
		/// <value>The identifier text.</value>
		public string IdText {
			get {
				return Client.clientId;
			}
		}

		/// <summary>
		/// Gets the friendly name text.
		/// </summary>
		/// <value>The friendly name text.</value>
		public string FriendlyNameText {
			get {
				return Client.friendlyName;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.ClientTableRowManager"/> friendly name label is hidden.
		/// </summary>
		/// <value><c>true</c> if friendly name label is hidden; otherwise, <c>false</c>.</value>
		public bool FriendlyNameLabelIsHidden {
			get {
				return string.IsNullOrWhiteSpace(Client.friendlyName);
			}
		}
	}
}
