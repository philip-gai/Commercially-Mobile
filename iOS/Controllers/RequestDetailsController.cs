using Foundation;
using System;
using UIKit;
using Commercially.iOS.Extensions;

namespace Commercially.iOS
{
	public partial class RequestDetailsController : UIViewController
	{
		public Request Request;

		public RequestDetailsController(IntPtr handle) : base(handle) { }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			SetLabelsFromRequest();
			StatusPickerView.Model = new StatusPickerViewModel();
		}

		void SetLabelsFromRequest()
		{
			DescriptionLabel.Text = Request.description;
			RoomLabel.Text = Request.room;
			UrgentIndicator.Hidden = !Request.urgent;
			ReceivedTimeLabel.Text = Request.GetTime(Request.TimeType.Received) ?? "N/A";
			AcceptedTimeLabel.Text = Request.GetTime(Request.TimeType.Schedule) ?? "N/A";
			CompletedTimeLabel.Text = Request.GetTime(Request.TimeType.Completed) ?? "N/A";
			StatusPickerView.Hidden = !(Request.assignedTo != null && Request.assignedTo.Equals(SessionData.User.email));
			AssignButton.Hidden = Request.GetStatus() != Status.New;
			SaveButton.Hidden = !(Request.assignedTo != null && Request.assignedTo.Equals(SessionData.User.email));
			ButtonStackView.Hidden = AssignButton.Hidden && SaveButton.Hidden;
			StatusLabel.Text = Request.GetStatus().ToString();
		}
	}
}