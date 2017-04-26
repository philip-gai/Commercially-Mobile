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

		public RequestStatusType? Type {
			get {
				return status.GetStatus();
			}
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

		public static Request[] GetRequests(RequestStatusType type)
		{
			var requests = RequestApi.GetRequests();
			if (requests == null || requests.Length <= 0) return null;
			var list = new List<Request>();
			foreach (var request in requests) {
				if (request.Type == type) {
					list.Add(request);
				}
			}
			list.Reverse();
			return list.ToArray();
		}
	}
}
