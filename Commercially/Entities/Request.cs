using System;
using System.Collections.Generic;
using System.Linq;

namespace Commercially
{
	[Serializable]
	public class Request
	{
		public enum TimeType { Received, Completed, Scheduled };

		public string room { get; set; }
		public string time_received { get; set; }
		public string time_completed { get; set; }
		public string time_scheduled { get; set; }
		public string status { get; set; }
		public string button_id { get; set; }
		public bool urgent { get; set; }
		public string _id { get; set; }
		public string description { get; set; }
		public string assignedTo { get; set; }

		public RequestStatusType Type {
			get {
				return GetStatusType(status);
			}
		}

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
