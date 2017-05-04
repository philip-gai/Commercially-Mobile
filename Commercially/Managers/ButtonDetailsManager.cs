// Created by Philip Gai

using Newtonsoft.Json.Linq;

namespace Commercially
{

	/// <summary>
	/// Button details manager.
	/// </summary>
	public class ButtonDetailsManager
	{
		/// <summary>
		/// The flic button whose details are being shown.
		/// </summary>
		public FlicButton Button;

		/// <summary>
		/// The client selected to pair with.
		/// </summary>
		public string SelectedClient;

		/// <summary>
		/// The duration of the animation.
		/// </summary>
		public const double AnimationDuration = 0.25;

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.ButtonDetailsManager"/> is paired.
		/// </summary>
		/// <value><c>true</c> if is paired; otherwise, <c>false</c>.</value>
		bool IsPaired {
			get {
				return !string.IsNullOrWhiteSpace(Button.clientId);
			}
		}

		/// <summary>
		/// Determines if the picker changed based on the original title.
		/// </summary>
		/// <returns><c>true</c>, if picker was changed, <c>false</c> otherwise.</returns>
		/// <param name="originalTitle">The original title for the picker.</param>
		public bool PickerDidChange(string originalTitle)
		{
			if (SelectedClient == null) return false;
			return !originalTitle.Equals(SelectedClient);
		}

		/// <summary>
		/// Determines if the button's location was edited.
		/// </summary>
		/// <returns><c>true</c>, if location was edited, <c>false</c> otherwise.</returns>
		/// <param name="locationText">The location text.</param>
		public bool LocationIsEdited(string locationText)
		{
			if (Button.room == null) return !string.IsNullOrWhiteSpace(locationText);
			return !Button.room.Equals(locationText);
		}

		/// <summary>
		/// Determines if the button's description was edited.
		/// </summary>
		/// <returns><c>true</c>, if description was edited, <c>false</c> otherwise.</returns>
		/// <param name="descriptionText">Description text.</param>
		public bool DescriptionIsEdited(string descriptionText)
		{
			if (Button.description == null) return !string.IsNullOrWhiteSpace(descriptionText);
			return !Button.description.Equals(descriptionText);
		}

		/// <summary>
		/// Gets the location field text.
		/// </summary>
		/// <value>The location field text.</value>
		public string LocationFieldText {
			get {
				return Button.room;
			}
		}

		/// <summary>
		/// Gets the description field text.
		/// </summary>
		/// <value>The description field text.</value>
		public string DescriptionFieldText {
			get {
				return Button.description;
			}
		}

		/// <summary>
		/// Gets the bluetooth identifier text.
		/// </summary>
		/// <value>The bluetooth identifier text.</value>
		public string BluetoothIdText {
			get {
				return Button.bluetooth_id;
			}
		}

		/// <summary>
		/// Gets the client identifier text.
		/// </summary>
		/// <value>The client identifier text. The client's friendly name if available.</value>
		public string ClientIdText {
			get {
				var tmpClient = ClientApi.GetClient(Button.clientId);
				return tmpClient != null && tmpClient.friendlyName != null ? tmpClient.friendlyName : Button.clientId;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.ButtonDetailsManager"/> client stack is hidden.
		/// </summary>
		/// <value><c>true</c> if client stack is hidden; otherwise, <c>false</c>.</value>
		public bool ClientStackIsHidden {
			get {
				return !IsPaired;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.ButtonDetailsManager"/> pair stack is hidden.
		/// </summary>
		/// <value><c>true</c> if pair stack is hidden; otherwise, <c>false</c>.</value>
		public bool PairStackIsHidden {
			get {
				return IsPaired;
			}
		}

		/// <summary>
		/// Saves the changes to the button through api calls.
		/// </summary>
		/// <returns><c>true</c>, if the button is being paired, <c>false</c> otherwise.</returns>
		/// <param name="locationText">Location text.</param>
		/// <param name="descriptionText">Description text.</param>
		/// <param name="pickerValue">Picker value.</param>
		public bool OnSaveButtonPressHandler(string locationText, string descriptionText, string pickerValue)
		{
			var jsonBody = new JObject();
			if (LocationIsEdited(locationText) && !string.IsNullOrWhiteSpace(locationText)) {
				jsonBody.Add("room", locationText);
			}
			if (DescriptionIsEdited(descriptionText) && !string.IsNullOrWhiteSpace(descriptionText)) {
				jsonBody.Add("description", descriptionText);
			}
			if (jsonBody.Count > 0) {
				FlicButtonApi.PatchButton(Button.bluetooth_id, jsonBody.ToString());
			}
			if (PickerDidChange(pickerValue)) {
				FlicButtonApi.PairButton(Button.bluetooth_id, ClientApi.GetClient(SelectedClient).clientId);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Gets the picker options.
		/// </summary>
		/// <returns>The picker options.</returns>
		/// <param name="button">Button.</param>
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
