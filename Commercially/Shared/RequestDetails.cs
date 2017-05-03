using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	public class RequestDetails
	{
		public Request Request;
		public string SelectedStatus;
		public string SelectedUser;

		public const double AnimationDuration = 0.25;
		public static Color DefaultTextColor = GlobalConstants.DefaultColors.Black;
		public static Color EditTextColor = GlobalConstants.DefaultColors.Red;

		bool StatusChanged {
			get {
				if (SelectedStatus == null) return false;
				return !Request.status.Equals(SelectedStatus, StringComparison.CurrentCultureIgnoreCase);
			}
		}
		bool UserChanged {
			get {
				if (SelectedUser == null) return false;
				return !SelectedUser.Equals(StartingUser, StringComparison.CurrentCultureIgnoreCase);
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
				return "Received:\n" + (time == null ? "N/A" : time.ToString());
			}
		}
		public string AcceptedTimeText {
			get {
				var time = Request.GetTime(Request.TimeType.Scheduled);
				return "Scheduled:\n" + (time == null ? "N/A" : time.ToString());
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
				return Request.Type != RequestStatusType.New || UserChanged;
			}
		}
		public bool SaveButtonIsHidden {
			get {
				return !StatusChanged && !UserChanged;
			}
		}
		public bool ButtonStackIsHidden {
			get {
				return AssignButtonIsHidden && !StatusChanged && !UserChanged;
			}
		}
		public bool StatusInputIsHidden {
			get {
				return Request.Type != RequestStatusType.Assigned;
			}
		}
		public bool StatusLabelIsHidden {
			get {
				return !StatusInputIsHidden;
			}
		}
		public bool UserPickerStackIsHidden {
			get {
				return Session.User.Type != UserRoleType.Admin || Request.Type == RequestStatusType.Completed || Request.Type == RequestStatusType.Cancelled;
			}
		}
		public string StartingUser {
			get {
				return string.IsNullOrWhiteSpace(Request.assignedTo) ? Localizable.Labels.NoneOption : Request.assignedTo;
			}
		}
		public string SaveStatusChanges()
		{
			return StatusChanged ? RequestApi.UpdateRequest(Request._id, Request.GetStatusType(SelectedStatus)) : null;
		}

		public string SaveUserChanges()
		{
			if (UserChanged) {
				var jsonBody = new JObject();
				if (SelectedUser.Equals(Localizable.Labels.NoneOption, StringComparison.CurrentCultureIgnoreCase)) {
					SelectedStatus = RequestStatusType.New.ToString();
					return SaveStatusChanges();
				}
				jsonBody.Add("assignedTo", SelectedUser);
				jsonBody.Add("status", RequestStatusType.Assigned.ToString().ToLower());
				jsonBody.Add("time_scheduled", DateTime.Now.ConvertToMilliseconds());
				return RequestApi.PatchRequest(Request._id, jsonBody.ToString());
			}
			return null;
		}

		public string AssignButtonPress()
		{
			return RequestApi.UpdateRequest(Request._id, RequestStatusType.Assigned);
		}

		public static string[] GetUserPickerOptions()
		{
			var users = UserApi.GetUsers();
			var usernameList = new List<string>();
			usernameList.Add(Localizable.Labels.NoneOption);
			foreach (var user in users) {
				usernameList.Add(user.username);
			}
			return usernameList.ToArray();
		}
	}
}
