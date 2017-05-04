// Created by Philip Gai

namespace Commercially
{
	/// <summary>
	/// Manages the business logic for a button table row.
	/// </summary>
	public class ButtonTableRowManager
	{
		/// <summary>
		/// The button for the row.
		/// </summary>
		public readonly FlicButton Button;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Commercially.ButtonTableRowManager"/> class.
		/// </summary>
		/// <param name="button">Button for the row.</param>
		public ButtonTableRowManager(FlicButton button)
		{
			Button = button;
		}

		/// <summary>
		/// Gets the button text.
		/// </summary>
		/// <value>The button text.</value>
		public string ButtonText {
			get {
				return Button.bluetooth_id.Substring(6);
			}
		}

		/// <summary>
		/// Gets the client text.
		/// </summary>
		/// <value>The client text.</value>
		public string ClientText {
			get {
				var tmpClient = ClientApi.GetClient(Button.clientId);
				return tmpClient != null && tmpClient.friendlyName != null ? tmpClient.friendlyName : Button.clientId;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.ButtonTableRowManager"/> client label is hidden.
		/// </summary>
		/// <value><c>true</c> if client label is hidden; otherwise, <c>false</c>.</value>
		public bool ClientLabelIsHidden {
			get {
				return string.IsNullOrWhiteSpace(Button.clientId);
			}
		}

		/// <summary>
		/// Gets the description text.
		/// </summary>
		/// <value>The description text.</value>
		public string DescriptionText {
			get {
				return Button.description;
			}
		}

		/// <summary>
		/// Gets the location text.
		/// </summary>
		/// <value>The location text.</value>
		public string LocationText {
			get {
				return Button.room;
			}
		}
	}
}
