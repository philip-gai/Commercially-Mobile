// Created by Philip Gai

using System;

namespace Commercially
{
	/// <summary>
	/// Represents a Flic Button.
	/// </summary>
	[Serializable]
	public class FlicButton
	{
		/// <summary>
		/// The description of what the flic button is for.
		/// </summary>
		public string description { get; set; }

		/// <summary>
		/// The bluetooth id for the flic button.
		/// Is the button's unique identifier.
		/// </summary>
		public string bluetooth_id { get; set; }

		/// <summary>
		/// The room the button is for.
		/// </summary>
		public string room { get; set; }

		/// <summary>
		/// The client the button is paired to.
		/// </summary>
		public string clientId { get; set; }

		/// <summary>
		/// All of the clientIds of the clients discovered by the button.
		/// </summary>
		public string[] discoveredBy { get; set; }

		/// <summary>
		/// Gets the type of a butotn based on its clientId.
		/// </summary>
		/// <value>The type.</value>
		public ButtonType Type {
			get {
				if (!string.IsNullOrWhiteSpace(clientId)) {
					if (GlobalConstants.Strings.Ignore.Equals(clientId, StringComparison.CurrentCultureIgnoreCase))
						return ButtonType.Ignored;
					return ButtonType.Paired;
				}
				return ButtonType.Discovered;
			}
		}

		/// <summary>
		/// Gets the discovered by clients from their clientIds.
		/// </summary>
		/// <value>The discovered by clients.</value>
		public Client[] DiscoveredByClients {
			get {
				return ClientApi.GetClients(discoveredBy);
			}
		}
	}
}