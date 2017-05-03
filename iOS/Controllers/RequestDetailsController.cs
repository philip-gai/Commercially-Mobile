using System;
using UIKit;

namespace Commercially.iOS
{
	public partial class RequestDetailsController : UIViewController
	{
		private readonly RequestDetails SharedController = new RequestDetails();

		public Request Request {
			set {
				SharedController.Request = value;
			}
		}

		public RequestDetailsController(IntPtr handle) : base(handle) { }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			InitializeView();
		}

		void InitializeView()
		{
			if (SharedController.Request == null) return;
			InitializeText();
			InitializeVisibility();
			InitializeStatusPickers();
		}

		void InitializeText()
		{
			DescriptionLabel.Text = SharedController.DescriptionText;
			RoomLabel.Text = SharedController.LocationText;
			StatusLabel.Text = SharedController.StatusText;
			AssignedToLabel.Text = SharedController.AssignedToText;
			ReceivedTimeLabel.Text = SharedController.ReceivedTimeText;
			AcceptedTimeLabel.Text = SharedController.AcceptedTimeText;
			CompletedTimeLabel.Text = SharedController.CompletedTimeText;
			StaticAssignLabel.TextColor = RequestDetails.EditTextColor.GetUIColor();
		}

		void InitializeVisibility()
		{
			UrgentIndicator.Hidden = SharedController.UrgentIndicatorIsHidden;
			AssignedToLabel.Hidden = SharedController.AssignedToIsHidden;
			AssignButton.Hidden = SharedController.AssignButtonIsHidden;
			SaveButton.Hidden = SharedController.SaveButtonIsHidden;
			ButtonStackView.Hidden = SharedController.ButtonStackIsHidden;
			StatusPickerView.Hidden = SharedController.StatusInputIsHidden;
			StatusLabel.Hidden = SharedController.StatusLabelIsHidden;
			AssignPickerStack.Hidden = SharedController.UserPickerStackIsHidden;
		}

		void InitializeStatusPickers()
		{
			StatusPickerView.Model = new StatusPickerModel(OnPickerChange);
			StatusPickerView.ScrollToTitle(SharedController.Request.Type.ToString());
			StaticStatusLabel.TextColor = SharedController.StatusInputIsHidden ? RequestDetails.DefaultTextColor.GetUIColor() : RequestDetails.EditTextColor.GetUIColor();

			UserPicker.Model = new UserPickerViewModel(OnPickerChange);
			SharedController.SelectedUser = SharedController.StartingUser;
			UserPicker.ScrollToTitle(SharedController.SelectedUser);
		}

		partial void SaveButtonPress(UIButton sender)
		{
			// Call Post to change request status string
			// Post for time that status was changed
			try {
				SharedController.SaveStatusChanges();
				SharedController.SaveUserChanges();
			} catch (Exception) {
				NavigationController.ShowPrompt(Localizable.PromptMessages.ChangesSaveError);
				return;
			}

			ButtonStackView.Hidden = AssignButton.Hidden;
			SaveButton.Hidden = true;
			NavigationController.PopViewController(true);
		}

		partial void AssignButtonPress(UIButton sender)
		{
			try {
				SharedController.AssignButtonPress();
			} catch (Exception) {
				NavigationController.ShowPrompt(Localizable.PromptMessages.AssignError);
				return;
			}

			UIView.AnimateAsync(RequestDetails.AnimationDuration, delegate {
				ButtonStackView.Hidden = SaveButton.Hidden;
			});
			AssignButton.Hidden = true;
			NavigationController.PopViewController(true);
		}

		void OnPickerChange(UIPickerView pickerView, nint row, nint component)
		{
			if (pickerView == StatusPickerView) {
				SharedController.SelectedStatus = pickerView.Model.GetTitle(pickerView, row, component);
			} else if (pickerView == UserPicker) {
				SharedController.SelectedUser = pickerView.Model.GetTitle(pickerView, row, component);
			}
			SaveButton.Hidden = SharedController.SaveButtonIsHidden;
			AssignButton.Hidden = SharedController.AssignButtonIsHidden;
			UIView.AnimateAsync(RequestDetails.AnimationDuration, delegate {
				ButtonStackView.Hidden = SharedController.ButtonStackIsHidden;
			});
		}
	}
}