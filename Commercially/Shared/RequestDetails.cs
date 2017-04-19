using System;
namespace Commercially
{
	public class RequestDetails
	{
		public const double AnimationDuration = 0.25;
		public static Color StaticStatusDefault = GlobalConstants.DefaultColors.Black;
		public static Color StaticStatusEdit = GlobalConstants.DefaultColors.Red;
		public Request Request;
		public string SelectedStatus;

		public bool IsMyRequest {
			get {
				return Request.assignedTo != null && Request.assignedTo.Equals(Session.User.email);
			}
		}
		public bool StatusChanged {
			get {
				return !Request.GetStatus().ToString().Equals(SelectedStatus);
			}
		}
		public string DescriptionText {
			get {
				return Request.description;	
			}
		}
		public string LocationText {
			get {
				return "Location: " + Request.room;	
			}
		}
		public bool UrgentIndicatorIsHidden {
			get {
				return !Request.urgent;	
			}
		}
		public string StatusText {
			get {
				return Request.GetStatus().ToString();	
			}
		}
		public bool AssignedToIsHidden {
			get {
				return string.IsNullOrWhiteSpace(Request.assignedTo);	
			}
		}
		public string AssignedToText {
			get {
				if (!AssignedToIsHidden) {
					return "Assigned To: " + Request.assignedTo;
				}
				return null;
			}
		}
		public string ReceivedTimeText {
			get {
				return "Received:\n" + Request.GetTime(Request.TimeType.Received) ?? "N/A";
			}
		}
		public string AcceptedTimeText {
			get {
				return "Scheduled:\n" + Request.GetTime(Request.TimeType.Scheduled) ?? "N/A";
			}
		}
		public string CompletedTimeText {
			get {
				return "Completed:\n" + Request.GetTime(Request.TimeType.Completed) ?? "N/A";
			}
		}
		public bool AssignButtonIsHidden {
			get {
				return Request.GetStatus() != RequestStatusType.New;
			}
		}
		public bool SaveButtonIsHidden {
			get {
				return !StatusChanged;
			}
		}
		public bool ButtonStackIsHidden {
			get {
				return AssignButtonIsHidden && !StatusChanged;
			}
		}
		public bool StatusInputIsHidden {
			get {
				return !IsMyRequest || (Request.GetStatus() == RequestStatusType.Completed || Request.GetStatus() == RequestStatusType.Cancelled);
			}
		}
		public bool StatusLabelIsHidden {
			get {
				return !StatusInputIsHidden;
			}
		}
	}
}
