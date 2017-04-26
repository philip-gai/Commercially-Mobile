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

		bool IsMyRequest {
			get {
				return Request.assignedTo != null && Request.assignedTo.Equals(Session.User.email);
			}
		}
		bool StatusChanged {
			get {
				if (SelectedStatus == null) return false;
				return !Request.status.ToString().Equals(SelectedStatus.ToLower());
			}
		}
		public string DescriptionText {
			get {
				return Request.description;	
			}
		}
		public string LocationText {
			get {
				return "Location:\n" + Request.room;	
			}
		}
		public bool UrgentIndicatorIsHidden {
			get {
				return !Request.urgent;	
			}
		}
		public string StatusText {
			get {
				return Request.Type.ToString();	
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
					return "Assigned To:\n" + Request.assignedTo;
				}
				return null;
			}
		}
		public string ReceivedTimeText {
			get {
				var time = Request.GetTime(Request.TimeType.Received);
				return "Received:\n" +  (time == null ? "N/A" : time.ToString());
			}
		}
		public string AcceptedTimeText {
			get {
				var time = Request.GetTime(Request.TimeType.Scheduled);
				return "Scheduled:\n" +  (time == null ? "N/A" : time.ToString());
			}
		}
		public string CompletedTimeText {
			get {
				var time = Request.GetTime(Request.TimeType.Completed);
				return "Completed:\n" + (time == null ? "N/A" : time.ToString());
			}
		}
		public bool AssignButtonIsHidden {
			get {
				return Request.Type != RequestStatusType.New;
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
				return !IsMyRequest || (Request.Type == RequestStatusType.Completed || Request.Type == RequestStatusType.Cancelled);
			}
		}
		public bool StatusLabelIsHidden {
			get {
				return !StatusInputIsHidden;
			}
		}
	}
}
