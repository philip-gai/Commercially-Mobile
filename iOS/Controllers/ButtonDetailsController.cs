using System;
using UIKit;
using Commercially.iOS.Extensions;
using Newtonsoft.Json.Linq;

namespace Commercially.iOS
{
	public partial class ButtonDetailsController : UIViewController
	{
		public ButtonDetailsController(IntPtr handle) : base(handle) { }

		const double AnimationDuration = 0.25;
		public FlicButton Button;
		string SelectedClient;

		bool IsPaired {
			get {
				return !string.IsNullOrWhiteSpace(Button.clientId);
			}
		}

		bool IsChanged {
			get {
				return PickerChanged || LocationChanged || DescriptionChanged;
			}
		}

		bool PickerChanged {
			get {
				if (ClientPickerView.Model == null) return false;
				return !ClientPickerView.Model.GetTitle(ClientPickerView, 0, 0).Equals(SelectedClient);
			}
		}

		bool LocationChanged {
			get {
				return !Button.room.Equals(LocationField.Text);
			}
		}

		bool DescriptionChanged {
			get {
				return !Button.description.Equals(DescriptionField.Text);
			}
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			NavigationItem.Title = "Details";
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			InitializeView();
		}

		partial void SaveButtonPress(UIButton sender)
		{
			try {
				var jsonBody = new JObject();
				if (LocationChanged && !string.IsNullOrWhiteSpace(LocationField.Text)) {
					jsonBody.Add("room", LocationField.Text);
				}
				if (DescriptionChanged && !string.IsNullOrWhiteSpace(DescriptionField.Text)) {
					jsonBody.Add("description", DescriptionField.Text);
				}
				if (jsonBody.Count > 0) {
					ButtonApi.PatchButton(Button.bluetooth_id, jsonBody.ToString());
				}
				if (PickerChanged) {
					ButtonApi.PairButton(Button.bluetooth_id, Client.FindClient(SelectedClient, SessionData.Clients).clientId);
				}
			} catch (Exception e) {
				NavigationController.ShowPrompt(e.Message, 50);
				return;
			}
			UIView.AnimateAsync(AnimationDuration, delegate {
				SaveButton.Hidden = true;
			});
			NavigationController.PopViewController(true);
		}

		void InitializeView()
		{
			if (Button == null) return;
			LocationField.Text = Button.room;
			DescriptionField.Text = Button.description;
			BluetoothIdLabel.Text = Button.bluetooth_id;
			LocationField.EditingChanged += OnFieldChange;
			DescriptionField.EditingChanged += OnFieldChange;
			ClientIdLabel.Hidden = !IsPaired;
			PairStack.Hidden = IsPaired;
			if (!PairStack.Hidden) {
				ClientPickerView.Model = new ClientPickerViewModel(FlicButton.GetDiscoveredByClients(Button), OnPickerChange);
			}
			if (!ClientIdLabel.Hidden) {
				ClientIdLabel.Text = Client.FindClient(Button.clientId, SessionData.Clients).friendlyName ?? Button.clientId;
			}
			SaveButton.Hidden = true;
		}

		void OnPickerChange(UIPickerView pickerView, nint row, nint component)
		{
			SelectedClient = pickerView.Model.GetTitle(pickerView, row, component);
			UIView.AnimateAsync(AnimationDuration, delegate {
				SaveButton.Hidden = !IsChanged;
			});
		}

		void OnFieldChange(object sender, EventArgs e)
		{
			UIView.AnimateAsync(AnimationDuration, delegate {
				SaveButton.Hidden = !IsChanged;
			});
		}
	}
}