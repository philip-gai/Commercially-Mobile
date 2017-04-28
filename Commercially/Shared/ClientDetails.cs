using System;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	public class ClientDetails
	{
		public Client Client;

		public const double AnimationDuration = 0.25;

		public bool FriendlyNameChanged(string friendlyNameText)
		{
			if (string.IsNullOrWhiteSpace(Client.friendlyName)) return true;
			return !Client.friendlyName.Equals(friendlyNameText);
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

		public string FriendlyNameFieldPlaceholder {
			get {
				return "Raspberry 1";
			}
		}

		public bool AuthorizeButtonIsHidden {
			get {
				return Client.authorized;
			}
		}

		public void SaveButtonPress(string friendlyName)
		{
			if (FriendlyNameChanged(friendlyName) && !string.IsNullOrWhiteSpace(friendlyName)) {
				ClientApi.PatchClientFriendlyName(Client.clientId, friendlyName);
			}
		}

		public void AuthorizeButtonPress()
		{
			ClientApi.AuthorizeClient(Client.clientId);
		}
	}
}
