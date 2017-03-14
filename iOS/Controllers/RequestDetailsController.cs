using Foundation;
using System;
using UIKit;

namespace Commercially.iOS
{
	public partial class RequestDetailsController : UIViewController
	{
		Request _Request;
		public Request Request {
			get {
				return _Request;
			}
			set {
				DescriptionLabel.Text = value.description;
				RoomLabel.Text = value.room;
				UrgentIndicator.Hidden = !value.urgent;
				ReceivedTimeLabel.Text = value.time_received.ConvertToDateTime().ToString();
				AcceptedTimeLabel.Text = value.time_scheduled.ConvertToDateTime().ToString() ?? "N/A";
				CompletedTimeLabel.Text = value.time_completed.ConvertToDateTime().ToString() ?? "N/A";
				AssignButton.Hidden = value.assignedTo == null;
				SaveButton.Enabled = false;
				_Request = value;
			}
		}
		public RequestDetailsController(IntPtr handle) : base(handle) { }
	}
}