﻿using System;
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
				var tmpClient = Client.FindClient(Button.clientId, Session.Clients);
				return tmpClient != null && tmpClient.friendlyName != null ? tmpClient.friendlyName : Button.clientId;
			}
		}

		public bool ClientIdIsHidden {
			get {
				return !IsPaired;
			}
		}

		public bool PairStackIsHidden {
			get {
				return IsPaired;
			}
		}
	}
}
