using System;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	public class ButtonDetails
	{
		public FlicButton Button;
		public string SelectedClient;

		public const double AnimationDuration = 0.25;

		bool IsPaired {
			get {
				return !string.IsNullOrWhiteSpace(Button.clientId);
			}
		}

		public bool PickerChanged(string originalTitle)
		{
			if (SelectedClient == null) return false;
			return !originalTitle.Equals(SelectedClient, StringComparison.CurrentCultureIgnoreCase);
		}

		public bool LocationChanged(string locationText)
		{
			if (Button.room == null) return !string.IsNullOrWhiteSpace(locationText);
			return !Button.room.Equals(locationText, StringComparison.CurrentCultureIgnoreCase);
		}

		public bool DescriptionChanged(string descriptionText)
		{
			if (Button.description == null) return !string.IsNullOrWhiteSpace(descriptionText);
			return !Button.description.Equals(descriptionText, StringComparison.CurrentCultureIgnoreCase);
		}

		public string LocationFieldText {
			get {
				return Button.room;
			}
		}

		public string DescriptionFieldText {
			get {
				return Button.description;
			}
		}

		public string BluetoothIdText {
			get {
				return Button.bluetooth_id;
			}
		}

		public string ClientIdText {
			get {
				var tmpClient = Client.FindClient(Button.clientId);
				return tmpClient != null && tmpClient.friendlyName != null ? tmpClient.friendlyName : Button.clientId;
			}
		}

		public bool ClientStackIsHidden {
			get {
				return !IsPaired;
			}
		}

		public bool PairStackIsHidden {
			get {
				return IsPaired;
			}
		}

		public void SaveButtonPress(string locationText, string descriptionText, string pickerValue)
		{
			var jsonBody = new JObject();
			if (LocationChanged(locationText) && locationText != null) {
				jsonBody.Add("room", locationText);
			}
			if (DescriptionChanged(descriptionText) && descriptionText != null) {
				jsonBody.Add("description", descriptionText);
			}
			if (jsonBody.Count > 0) {
				FlicButtonApi.PatchButton(Button.bluetooth_id, jsonBody.ToString());
			}
			if (PickerChanged(pickerValue)) {
				FlicButtonApi.PairButton(Button.bluetooth_id, Client.FindClient(SelectedClient).clientId);
			}
		}

		public static string[] GetPickerOptions(FlicButton button)
		{
			Client[] discoveredByClients = button.DiscoveredByClients;
			string[] tmpDiscoveredBy = new string[button.discoveredBy.Length + 2];
			tmpDiscoveredBy[0] = Localizable.Labels.NoneOption;
			tmpDiscoveredBy[1] = GlobalConstants.Strings.Ignore;
			for (int i = 0; i < discoveredByClients.Length; i++) {
				var client = discoveredByClients[i];
				tmpDiscoveredBy[i + 2] = string.IsNullOrWhiteSpace(client.friendlyName) ? client.clientId : client.friendlyName;
			}
			return tmpDiscoveredBy;
		}
	}
}
