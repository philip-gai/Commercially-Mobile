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

		string NewStatus;

		public RequestDetailsController(IntPtr handle) : base(handle) { }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			SetInfo();
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			NavigationItem.Title = "Details";
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
		}

		partial void SaveChangesButtonPress(UIButton sender)
		{
			// Call Post to change request status string
			// Post for time that status was changed
			try {
				RequestApi.UpdateRequest(Request._id, (RequestStatusType)NewStatus.GetStatus());
			} catch (Exception e) {
				NavigationController.ShowPrompt(e.Message);
				return;
			}
			UIView.AnimateAsync(AnimationDuration, delegate {
				ButtonStackView.Hidden = AssignButton.Hidden;
			});
			SaveButton.Hidden = true;
		}

		void SetInfo()
		{
			DescriptionLabel.Text = Request.description;
			RoomLabel.Text = Request.room;
			UrgentIndicator.Hidden = !Request.urgent;
			SetAssignedTo();
			SetDateTimes();
			SetStatusPicker();
			SetButtonsVisible();
			SetStatusLabel();
		}

		void SetAssignedTo()
		{
			AssignedToLabel.Hidden = string.IsNullOrWhiteSpace(Request.assignedTo);
			if (!string.IsNullOrWhiteSpace(Request.assignedTo)) {
				AssignedToLabel.Text = "Assigned To: " + Request.assignedTo;
			}
		}

		void SetDateTimes()
		{
			ReceivedTimeLabel.Text = Request.GetTime(Request.TimeType.Received) ?? "N/A";
			AcceptedTimeLabel.Text = Request.GetTime(Request.TimeType.Scheduled) ?? "N/A";
			CompletedTimeLabel.Text = Request.GetTime(Request.TimeType.Completed) ?? "N/A";
		}

		void SetStatusPicker()
		{
			StatusPickerView.Model = new StatusPickerViewModel(OnPickerChange);
			StatusPickerView.Hidden = !IsMyRequest;
			if (!StatusStackView.Hidden) {
				// Scroll to current status in picker view
				StatusPickerView.ScrollToTitle(Request.GetStatus().ToString());
			}
		}

		void OnPickerChange(UIPickerView pickerView, nint row, nint component)
		{
			NewStatus = pickerView.Model.GetTitle(pickerView, row, component).ToLower();
			SaveButton.Hidden = Request.GetStatus().ToString().Equals(NewStatus);
			UIView.AnimateAsync(AnimationDuration, delegate {
				ButtonStackView.Hidden = AssignButton.Hidden && SaveButton.Hidden;
			});
		}

		void SetButtonsVisible()
		{
			AssignButton.Hidden = Request.GetStatus() != RequestStatusType.New;
			SaveButton.Hidden = true;
			ButtonStackView.Hidden = AssignButton.Hidden && SaveButton.Hidden;
		}

		void SetStatusLabel()
		{
			if (Request.GetStatus() == RequestStatusType.New) {
				StatusLabel.Hidden = false;
				StatusLabel.Text = Request.GetStatus().ToString();
			} else {
				StatusLabel.Hidden = true;
			}
		}
	}
}