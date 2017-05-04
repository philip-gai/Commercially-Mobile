// Created by Philip Gai

using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Commercially
{
	/// <summary>
	/// Request details manager.
	/// </summary>
	public class RequestDetailsManager
	{
		/// <summary>
		/// The request.
		/// </summary>
		public Request Request;

		/// <summary>
		/// The selected status from the picker.
		/// </summary>
		public string SelectedStatus;

		/// <summary>
		/// The selected user from the picker.
		/// </summary>
		public string SelectedUser;

		/// <summary>
		/// The duration of the animation.
		/// </summary>
		public const double AnimationDuration = 0.25;

		/// <summary>
		/// The default color of the text.
		/// </summary>
		public static Color DefaultTextColor = GlobalConstants.DefaultColors.Black;

		/// <summary>
		/// The color of the edit text.
		/// </summary>
		public static Color EditTextColor = GlobalConstants.DefaultColors.Red;

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.RequestDetailsManager"/> status is changed.
		/// </summary>
		/// <value><c>true</c> if status is changed; otherwise, <c>false</c>.</value>
		bool StatusIsChanged {
			get {
				if (SelectedStatus == null) return false;
				return !Request.status.Equals(SelectedStatus, StringComparison.CurrentCultureIgnoreCase);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.RequestDetailsManager"/> user is changed.
		/// </summary>
		/// <value><c>true</c> if user is changed; otherwise, <c>false</c>.</value>
		bool UserIsChanged {
			get {
				if (SelectedUser == null) return false;
				return !SelectedUser.Equals(StartingUser, StringComparison.CurrentCultureIgnoreCase);
			}
		}

		/// <summary>
		/// Gets the description text.
		/// </summary>
		/// <value>The description text.</value>
		public string DescriptionText {
			get {
				return Request.description;
			}
		}

		/// <summary>
		/// Gets the location text.
		/// </summary>
		/// <value>The location text.</value>
		public string LocationText {
			get {
				return "Location:\n" + Request.room;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.RequestDetailsManager"/> urgent indicator is hidden.
		/// </summary>
		/// <value><c>true</c> if urgent indicator is hidden; otherwise, <c>false</c>.</value>
		public bool UrgentIndicatorIsHidden {
			get {
				return !Request.urgent;
			}
		}

		/// <summary>
		/// Gets the status text.
		/// </summary>
		/// <value>The status text.</value>
		public string StatusText {
			get {
				return Request.Type.ToString();
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.RequestDetailsManager"/> assigned to text is hidden.
		/// </summary>
		/// <value><c>true</c> if assigned to text is hidden; otherwise, <c>false</c>.</value>
		public bool AssignedToTextIsHidden {
			get {
				return string.IsNullOrWhiteSpace(Request.assignedTo);
			}
		}

		/// <summary>
		/// Gets the assigned to text.
		/// </summary>
		/// <value>The assigned to text.</value>
		public string AssignedToText {
			get {
				if (!AssignedToTextIsHidden) {
					return "Assigned To:\n" + Request.assignedTo;
				}
				return null;
			}
		}

		/// <summary>
		/// Gets the received time text.
		/// </summary>
		/// <value>The received time text.</value>
		public string ReceivedTimeText {
			get {
				var time = Request.GetTime(Request.TimeType.Received);
				return "Received:\n" + (time == null ? "N/A" : time.ToString());
			}
		}

		/// <summary>
		/// Gets the accepted time text.
		/// </summary>
		/// <value>The accepted time text.</value>
		public string AcceptedTimeText {
			get {
				var time = Request.GetTime(Request.TimeType.Scheduled);
				return "Scheduled:\n" + (time == null ? "N/A" : time.ToString());
			}
		}

		/// <summary>
		/// Gets the completed time text.
		/// </summary>
		/// <value>The completed time text.</value>
		public string CompletedTimeText {
			get {
				var time = Request.GetTime(Request.TimeType.Completed);
				return "Completed:\n" + (time == null ? "N/A" : time.ToString());
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.RequestDetailsManager"/> assign button is hidden.
		/// </summary>
		/// <value><c>true</c> if assign button is hidden; otherwise, <c>false</c>.</value>
		public bool AssignButtonIsHidden {
			get {
				return Request.Type != RequestStatusType.New || UserIsChanged;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.RequestDetailsManager"/> save button is hidden.
		/// </summary>
		/// <value><c>true</c> if save button is hidden; otherwise, <c>false</c>.</value>
		public bool SaveButtonIsHidden {
			get {
				return !StatusIsChanged && !UserIsChanged;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.RequestDetailsManager"/> button stack is hidden.
		/// </summary>
		/// <value><c>true</c> if button stack is hidden; otherwise, <c>false</c>.</value>
		public bool ButtonStackIsHidden {
			get {
				return AssignButtonIsHidden && !StatusIsChanged && !UserIsChanged;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.RequestDetailsManager"/> status input is hidden.
		/// </summary>
		/// <value><c>true</c> if status input is hidden; otherwise, <c>false</c>.</value>
		public bool StatusInputIsHidden {
			get {
				return Request.Type != RequestStatusType.Assigned;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.RequestDetailsManager"/> status label is hidden.
		/// </summary>
		/// <value><c>true</c> if status label is hidden; otherwise, <c>false</c>.</value>
		public bool StatusLabelIsHidden {
			get {
				return !StatusInputIsHidden;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Commercially.RequestDetailsManager"/> user picker stack is hidden.
		/// </summary>
		/// <value><c>true</c> if user picker stack is hidden; otherwise, <c>false</c>.</value>
		public bool UserPickerStackIsHidden {
			get {
				return Session.User.Type != UserRoleType.Admin || Request.Type == RequestStatusType.Completed || Request.Type == RequestStatusType.Cancelled;
			}
		}

		/// <summary>
		/// Gets the starting user.
		/// </summary>
		/// <value>The starting user.</value>
		public string StartingUser {
			get {
				return string.IsNullOrWhiteSpace(Request.assignedTo) ? Localizable.Labels.NoneOption : Request.assignedTo;
			}
		}

		/// <summary>
		/// Saves the status changes.
		/// </summary>
		/// <returns>The api response.</returns>
		public string SaveStatusChanges()
		{
			return StatusIsChanged ? RequestApi.UpdateRequest(Request._id, Request.GetStatusType(SelectedStatus)) : null;
		}

		/// <summary>
		/// Saves the user changes.
		/// </summary>
		/// <returns>The api response.</returns>
		public string SaveUserChanges()
		{
			if (UserIsChanged) {
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

		/// <summary>
		/// Handles assign button presses.
		/// </summary>
		/// <returns>The api response.</returns>
		public string OnAssignButtonPressHandler()
		{
			return RequestApi.UpdateRequest(Request._id, RequestStatusType.Assigned);
		}

		/// <summary>
		/// Gets the user picker options.
		/// </summary>
		/// <returns>The user picker options.</returns>
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
