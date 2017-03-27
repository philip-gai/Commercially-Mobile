using System;
using UIKit;
using Newtonsoft.Json.Linq;
using Commercially.iOS.Extensions;

namespace Commercially.iOS
{
	public partial class RequestDetailsController : UIViewController
	{
		const double AnimationDuration = 0.25;
		public Request Request;
		bool IsMyRequest {
			get {
				return Request.assignedTo != null && Request.assignedTo.Equals(SessionData.User.email);
			}
		}
		bool StatusChanged {
			get {
				return !Request.GetStatus().ToString().Equals(SelectedStatus);
			}
		}

		string SelectedStatus;

		public RequestDetailsController(IntPtr handle) : base(handle) { }

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
			// Call Post to change request status string
			// Post for time that status was changed
			try {
				RequestApi.UpdateRequest(Request._id, (RequestStatusType)SelectedStatus.GetStatus());
			} catch (Exception e) {
				NavigationController.ShowPrompt(e.Message);
				return;
			}
			UIView.AnimateAsync(AnimationDuration, delegate {
				ButtonStackView.Hidden = AssignButton.Hidden;
			});
			SaveButton.Hidden = true;
			NavigationController.PopViewController(true);
		}

		partial void AssignButtonPress(UIButton sender)
		{
			// Call Post to change button ownedBy value to this user's email in DB
			try {
				RequestApi.UpdateRequest(Request._id, RequestStatusType.Assigned);
			} catch (Exception e) {
				NavigationController.ShowPrompt(e.Message);
				return;
			}

			UIView.AnimateAsync(AnimationDuration, delegate {
				ButtonStackView.Hidden = SaveButton.Hidden;
			});
			AssignButton.Hidden = true;
			NavigationController.PopViewController(true);
		}

		void InitializeView()
		{
			if (Request == null) return;
			DescriptionLabel.Text = Request.description;
			RoomLabel.Text = "Location: " + Request.room;
			UrgentIndicator.Hidden = !Request.urgent;
			StatusLabel.Text = Request.GetStatus().ToString();
			InitializeAssignedTo();
			InitializeDateTimes();
			InitializeVisibility();
			InitializeStatusPicker();
		}

		void InitializeAssignedTo()
		{
			AssignedToLabel.Hidden = string.IsNullOrWhiteSpace(Request.assignedTo);
			if (!string.IsNullOrWhiteSpace(Request.assignedTo)) {
				AssignedToLabel.Text = "Assigned To: " + Request.assignedTo;
			}
		}

		void InitializeDateTimes()
		{
			ReceivedTimeLabel.Text = "Received:\n" + Request.GetTime(Request.TimeType.Received) ?? "N/A";
			AcceptedTimeLabel.Text = "Scheduled:\n" + Request.GetTime(Request.TimeType.Scheduled) ?? "N/A";
			CompletedTimeLabel.Text = "Completed:\n" + Request.GetTime(Request.TimeType.Completed) ?? "N/A";
		}

		void InitializeVisibility()
		{
			AssignButton.Hidden = Request.GetStatus() != RequestStatusType.New;
			SaveButton.Hidden = true;
			ButtonStackView.Hidden = AssignButton.Hidden && SaveButton.Hidden;
			StatusPickerView.Hidden = !IsMyRequest || (Request.GetStatus() == RequestStatusType.Completed || Request.GetStatus() == RequestStatusType.Cancelled);
			StatusLabel.Hidden = !StatusPickerView.Hidden;
		}

		void InitializeStatusPicker()
		{
			StatusPickerView.Model = new StatusPickerViewModel(OnPickerChange);
			StaticStatusLabel.TextColor = UIColor.Black;
			if (!StatusPickerView.Hidden) {
				// Scroll to current status in picker view
				StatusPickerView.ScrollToTitle(Request.GetStatus().ToString());
				StaticStatusLabel.TextColor = GlobalConstants.DefaultColors.Red.GetUIColor();
			}
		}

		void OnPickerChange(UIPickerView pickerView, nint row, nint component)
		{
			SelectedStatus = pickerView.Model.GetTitle(pickerView, row, component);
			SaveButton.Hidden = !StatusChanged;
			UIView.AnimateAsync(AnimationDuration, delegate {
				ButtonStackView.Hidden = AssignButton.Hidden && SaveButton.Hidden;
			});
		}
	}
}