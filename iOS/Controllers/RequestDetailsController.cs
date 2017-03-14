using Foundation;
using System;
using UIKit;
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
			UIView.AnimateAsync(AnimationDuration, delegate {
				ButtonStackView.Hidden = SaveButton.Hidden;
			});
			AssignButton.Hidden = true;
		}

		partial void SaveChangesButtonPress(UIButton sender)
		{
			// Call Post to change request status stringg
			// Post for time that status was changed
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
			AcceptedTimeLabel.Text = Request.GetTime(Request.TimeType.Schedule) ?? "N/A";
			CompletedTimeLabel.Text = Request.GetTime(Request.TimeType.Completed) ?? "N/A";
		}

		void SetStatusPicker()
		{
			StatusPickerView.Model = new StatusPickerViewModel(OnPickerChange);
			StatusPickerView.Hidden = !IsMyRequest;
			if (!StatusStackView.Hidden) {
				// Scroll to current status in picker viewr
				StatusPickerView.ScrollToTitle(Request.GetStatus().ToString());
			}
		}

		void OnPickerChange(UIPickerView pickerView, nint row, nint component)
		{
			SaveButton.Hidden = pickerView.IsTitleMatch(Request.GetStatus().ToString(), row, component);
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