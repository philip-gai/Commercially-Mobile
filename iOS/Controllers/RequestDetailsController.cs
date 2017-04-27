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

		partial void SaveButtonPress(UIButton sender)
		{
			// Call Post to change request status string
			// Post for time that status was changed
			try {
				RequestApi.UpdateRequest(SharedController.Request._id, Request.GetStatusType(SharedController.SelectedStatus));
			} catch (Exception) {
				NavigationController.ShowPrompt(Localizable.PromptMessages.RequestSaveError);
				return;
			}
			UIView.AnimateAsync(RequestDetails.AnimationDuration, delegate {
				ButtonStackView.Hidden = AssignButton.Hidden;
			});
			SaveButton.Hidden = true;
			NavigationController.PopViewController(true);
		}

		partial void AssignButtonPress(UIButton sender)
		{
			// Call Post to change button ownedBy value to this user's email in DB
			try {
				RequestApi.UpdateRequest(SharedController.Request._id, RequestStatusType.Assigned);
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

		void InitializeView()
		{
			if (SharedController.Request == null) return;
			InitializeText();
			InitializeVisibility();
			InitializeStatusPicker();
		}

		void InitializeText() {
			DescriptionLabel.Text = SharedController.DescriptionText;
			RoomLabel.Text = SharedController.LocationText;
			StatusLabel.Text = SharedController.StatusText;
			AssignedToLabel.Text = SharedController.AssignedToText;
			ReceivedTimeLabel.Text = SharedController.ReceivedTimeText;
			AcceptedTimeLabel.Text = SharedController.AcceptedTimeText;
			CompletedTimeLabel.Text = SharedController.CompletedTimeText;
		}

		void InitializeVisibility() {
			UrgentIndicator.Hidden = SharedController.UrgentIndicatorIsHidden;
			AssignedToLabel.Hidden = SharedController.AssignedToIsHidden;
			AssignButton.Hidden = SharedController.AssignButtonIsHidden;
			SaveButton.Hidden = SharedController.SaveButtonIsHidden;
			ButtonStackView.Hidden = SharedController.ButtonStackIsHidden;
			StatusPickerView.Hidden = SharedController.StatusInputIsHidden;
			StatusLabel.Hidden = SharedController.StatusLabelIsHidden;
		}

		void InitializeStatusPicker()
		{
			StatusPickerView.Model = new StatusPickerViewModel(OnPickerChange);
			StaticStatusLabel.TextColor = RequestDetails.StaticStatusDefault.GetUIColor();
			if (!SharedController.StatusInputIsHidden) {
				// Scroll to current status in picker view
				StatusPickerView.ScrollToTitle(SharedController.Request.Type.ToString());
				StaticStatusLabel.TextColor = RequestDetails.StaticStatusEdit.GetUIColor();
			}
		}

		void OnPickerChange(UIPickerView pickerView, nint row, nint component)
		{
			SharedController.SelectedStatus = pickerView.Model.GetTitle(pickerView, row, component);
			SaveButton.Hidden = SharedController.SaveButtonIsHidden;
			UIView.AnimateAsync(RequestDetails.AnimationDuration, delegate {
				ButtonStackView.Hidden = SharedController.ButtonStackIsHidden;
			});
		}
	}
}