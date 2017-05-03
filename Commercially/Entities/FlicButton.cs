using System;

namespace Commercially
{
	[Serializable]
	public class FlicButton
	{
		public string description { get; set; }
		public string bluetooth_id { get; set; }
		public string room { get; set; }
		public string clientId { get; set; }
		public string[] discoveredBy { get; set; }

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

		public Client[] DiscoveredByClients {
			get {
				return ClientApi.GetClients(discoveredBy);
			}
		}
	}
}
