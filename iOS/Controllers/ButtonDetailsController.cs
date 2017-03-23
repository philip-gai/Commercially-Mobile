using Foundation;
using System;
using UIKit;
using Commercially.iOS.Extensions;

namespace Commercially.iOS
{
	public partial class ButtonDetailsController : UIViewController
	{
		public ButtonDetailsController(IntPtr handle) : base(handle) { }

		public FlicButton Button;
		string NewClientFriendlyName;

		bool IsPaired {
			get {
				return !string.IsNullOrWhiteSpace(Button.clientId);
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
			SetInfo();
		}

		void SetInfo()
		{
			if (Button == null) return;
			LocationField.Text = Button.room;
			DescriptionField.Text = Button.description;
			BluetoothIdLabel.Text = Button.bluetooth_id;
			if (IsPaired) {
				ClientIdLabel.Text = Client.FindFriendlyName(SessionData.Clients, Button.clientId);
			}

			LocationField.SetPlaceholderColor(LocationField.TextColor);
			DescriptionField.SetPlaceholderColor(DescriptionField.TextColor);
		}

		void SetVisibility()
		{
			ClientIdLabel.Hidden = !IsPaired;
			ClientPickerView.Hidden = IsPaired;
		}

		void SetClientPicker()
		{
			ClientPickerView.Model = new ClientPickerViewModel(OnPickerChange);
			//if (!StatusPickerView.Hidden) {
			//	// Scroll to current status in picker view
			//	StatusPickerView.ScrollToTitle(Request.GetStatus().ToString());
			//	StaticStatusLabel.TextColor = GlobalConstants.DefaultColors.Red.GetUIColor();
			//}
		}

		void OnPickerChange(UIPickerView pickerView, nint row, nint component)
		{
			NewClientFriendlyName = pickerView.Model.GetTitle(pickerView, row, component).ToLower();
			//SaveButton.Hidden = Request.GetStatus().ToString().Equals(NewStatus);
			//UIView.AnimateAsync(AnimationDuration, delegate {
				//ButtonStackView.Hidden = AssignButton.Hidden && SaveButton.Hidden;
			//});
		}
	}
}