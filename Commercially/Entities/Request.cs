// Created by Philip Gai

using System;

namespace Commercially
{
	/// <summary>
	/// Represents a request made by a flic button.
	/// </summary>
	[Serializable]
	public class Request
	{
		/// <summary>
		/// The type of timestamp from a request.
		/// </summary>
		public enum TimeType { Received, Completed, Scheduled };

		/// <summary>
		/// The room for the request.
		/// </summary>
		public string room { get; set; }

		/// <summary>
		/// The time the request was received by the system.
		/// </summary>
		public string time_received { get; set; }

		/// <summary>
		/// The time the request was completed by a worker.
		/// </summary>
		public string time_completed { get; set; }

		/// <summary>
		/// The time a request was scheduled / assigned to a worker.
		/// </summary>
		public string time_scheduled { get; set; }

		/// <summary>
		/// The status of the request (new, assigned, completed or cancelled).
		/// </summary>
		public string status { get; set; }

		/// <summary>
		/// The unique button identifier.
		/// </summary>
		public string button_id { get; set; }

		/// <summary>
		/// A value indicating whether this <see cref="T:Commercially.Request"/> is urgent.
		/// </summary>
		public bool urgent { get; set; }

		/// <summary>
		/// The id given to the request by the MongoDB.
		/// </summary>
		public string _id { get; set; }

		/// <summary>
		/// The description of the request.
		/// </summary>
		public string description { get; set; }

		/// <summary>
		/// The username of who the request is assigned to.
		/// </summary>
		public string assignedTo { get; set; }

		/// <summary>
		/// Gets the type of status based on the status string.
		/// </summary>
		public RequestStatusType Type {
			get {
				return GetStatusType(status);
			}
		}

		/// <summary>
		/// Gets the type of the status based on the status string.
		/// </summary>
		/// <returns>The status type.</returns>
		/// <param name="status">The status string.</param>
		public static RequestStatusType GetStatusType(string status)
		{
			if (status.Equals(RequestStatusType.New.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
				return RequestStatusType.New;
			}
			if (status.Equals(RequestStatusType.Assigned.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
				return RequestStatusType.Assigned;
			}
			if (status.Equals(RequestStatusType.Completed.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
				return RequestStatusType.Completed;
			}
			if (status.Equals(RequestStatusType.Cancelled.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
				return RequestStatusType.Cancelled;
			}
			return RequestStatusType.New;
		}

		/// <summary>
		/// Gets the time based on what type of time is wanted.
		/// </summary>
		/// <returns>The time.</returns>
		/// <param name="type">The type of time sought.</param>
		public DateTime? GetTime(TimeType type)
		{
			DateTime? time = null;
			switch (type) {
				case TimeType.Received:
					time = time_received.ConvertToDateTime();
					break;
				case TimeType.Scheduled:
					time = time_scheduled.ConvertToDateTime();
					break;
				case TimeType.Completed:
					time = time_completed.ConvertToDateTime();
					break;
			}
			return time;
		}
	}
}
