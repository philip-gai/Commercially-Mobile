namespace Commercially
{
	public class ButtonTableRow
	{
		public readonly FlicButton Button;

		public ButtonTableRow(FlicButton button)
		{
			Button = button;
		}

		public string ButtonText {
			get {
				return Button.bluetooth_id.Substring(6);
			}
		}

		public string ClientText {
			get {
				var tmpClient = Client.FindClient(Button.clientId);
				return tmpClient != null && tmpClient.friendlyName != null ? tmpClient.friendlyName : Button.clientId;
			}
		}

		public bool ClientLabelIsHidden {
			get {
				return string.IsNullOrWhiteSpace(Button.clientId);
			}
		}

		public string DescriptionText {
			get {
				return Button.description;
			}
		}

		public string LocationText {
			get {
				return Button.room;
			}
		}
	}
}
