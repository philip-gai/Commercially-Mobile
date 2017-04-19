using System;
using UIKit;
using Commercially.iOS.Extensions;
using Newtonsoft.Json.Linq;

namespace Commercially.iOS
{
	public partial class ButtonDetailsController : KeyboardController
	{
		private readonly ButtonDetails SharedController = new ButtonDetails();

		public FlicButton Button {
			set {
				SharedController.Button = value;
			}
		}

		public ButtonDetailsController(IntPtr handle) : base(handle) { }

		bool IsChanged {
			get {
				return PickerChanged || LocationChanged || DescriptionChanged;
			}
		}

		bool PickerChanged {
			get {
				if (ClientPickerView.Model == null) return false;
				return !ClientPickerView.Model.GetTitle(ClientPickerView, 0, 0).Equals(SharedController.SelectedClient);
			}
		}

		bool LocationChanged {
			get {
				if (SharedController.Button.room == null) return !string.IsNullOrWhiteSpace(LocationField.Text);
				return !SharedController.Button.room.Equals(LocationField.Text);
			}
		}

		bool DescriptionChanged {
			get {
				if (SharedController.Button.description == null) return !string.IsNullOrWhiteSpace(DescriptionField.Text);
				return !SharedController.Button.description.Equals(DescriptionField.Text);
			}
		}

		public override UIScrollView ScrollView {
			get {
				return KeyboardScrollView;
			}
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
				if (LocationChanged && LocationField.Text != null) {
					jsonBody.Add("room", LocationField.Text);
				}
				if (DescriptionChanged && DescriptionField.Text != null) {
					jsonBody.Add("description", DescriptionField.Text);
				}
				if (jsonBody.Count > 0) {
					ButtonApi.PatchButton(SharedController.Button.bluetooth_id, jsonBody.ToString());
				}
				if (PickerChanged) {
					ButtonApi.PairButton(SharedController.Button.bluetooth_id, Client.FindClient(SharedController.SelectedClient, Session.Clients).clientId);
				}
			} catch (Exception) {
				NavigationController.ShowPrompt(Localizable.PromptMessages.ButtonSaveError);
				return;
			}
			UIView.AnimateAsync(ButtonDetails.AnimationDuration, delegate {
				SaveButton.Hidden = true;
			});
			NavigationController.PopViewController(true);
		}

		void InitializeView()
		{
			if (SharedController.Button == null) return;
			LocationField.ShouldReturn += (textField) => { LocationField.ResignFirstResponder(); return true; };
			DescriptionField.ShouldReturn += (textField) => { DescriptionField.ResignFirstResponder(); return true; };
			LocationField.Text = SharedController.LocationFieldText;
			DescriptionField.Text = SharedController.DescriptionFieldText;
			BluetoothIdLabel.Text = SharedController.BluetoothIdText;
			LocationField.EditingChanged += OnFieldChange;
			DescriptionField.EditingChanged += OnFieldChange;
			ClientIdLabel.Hidden = SharedController.ClientIdIsHidden;
			PairStack.Hidden = SharedController.PairStackIsHidden;
			if (!PairStack.Hidden) {
				ClientPickerView.Model = new ClientPickerViewModel(FlicButton.GetDiscoveredByClients(SharedController.Button), OnPickerChange);
			}
			if (!ClientIdLabel.Hidden) {
				ClientIdLabel.Text = SharedController.ClientIdText;
			}
			SaveButton.Hidden = true;
		}

		void OnPickerChange(UIPickerView pickerView, nint row, nint component)
		{
			SharedController.SelectedClient = pickerView.Model.GetTitle(pickerView, row, component);
			UIView.AnimateAsync(ButtonDetails.AnimationDuration, delegate {
				SaveButton.Hidden = !IsChanged;
			});
		}

		void OnFieldChange(object sender, EventArgs e)
		{
			UIView.AnimateAsync(ButtonDetails.AnimationDuration, delegate {
				SaveButton.Hidden = !IsChanged;
			});
		}
	}
}