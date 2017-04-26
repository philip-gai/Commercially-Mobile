using System;
using System.Collections.Generic;

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
				if (!string.IsNullOrWhiteSpace(clientId)) return ButtonType.Paired;
				if (clientId.Equals(GlobalConstants.Strings.Ignore)) return ButtonType.Ignored;
				return ButtonType.Discovered;
			}
		}

		public static Client[] GetDiscoveredByClients(FlicButton Button)
		{
			if (Button == null || Button.discoveredBy == null || Session.Clients == null) return null;
			Client[] clients = new Client[Button.discoveredBy.Length];
			for (int i = 0; i < Button.discoveredBy.Length; i++) {
				clients[i] = Client.FindClient(Button.discoveredBy[i], Session.Clients);
			}
			return clients;
		}

		public static FlicButton[] GetButtons(ButtonType type)
		{
			var buttons = ButtonApi.GetButtons();
			if (buttons == null || buttons.Length <= 0) return null;
			var list = new List<FlicButton>();
			foreach (var button in buttons) {
				if (button.Type == type) {
					list.Add(button);
				}
			}
			list.Reverse();
			return list.ToArray();
		}
	}
}
