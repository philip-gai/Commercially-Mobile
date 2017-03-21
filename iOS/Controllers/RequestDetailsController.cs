using Foundation;
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

		string currentStatus;

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
				RequestApi.ClaimRequest(Request._id);
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
			JObject jsonBody = new JObject();
			// Call Post to change request status string
			// Post for time that status was changed
			jsonBody.Add("status", currentStatus.ToLower());
			if (currentStatus.ToLower().Equals("new")) {
				jsonBody.Add("assignedTo", "");
			}
			try {
				RequestApi.PatchRequest(Request._id, jsonBody.ToString());
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
			SetDateTimes();
			SetStatusPicker();
			SetButtonsVisible();
			SetStatusLabel();
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
			currentStatus = pickerView.Model.GetTitle(pickerView, row, component);
			SaveButton.Hidden = Request.GetStatus().ToString().Equals(currentStatus);
			UIView.AnimateAsync(AnimationDuration, delegate {
				ButtonStackView.Hidden = AssignButton.Hidden && SaveButton.Hidden;
			});
		}

		void SetButtonsVisible()
		{
			AssignButton.Hidden = Request.GetStatus() != Status.New;
			SaveButton.Hidden = true;
			ButtonStackView.Hidden = AssignButton.Hidden && SaveButton.Hidden;
		}

		void SetStatusLabel()
		{
			if (Request.GetStatus() == Status.New) {
				StatusLabel.Hidden = false;
				StatusLabel.Text = Request.GetStatus().ToString();
			} else {
				StatusLabel.Hidden = true;
			}
		}
	}
}