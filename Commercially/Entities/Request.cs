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
		public string time_completed { get; set; }
		public string time_scheduled { get; set; }
		public string status { get; set; }
		public string button_id { get; set; }
		public bool urgent { get; set; }
		public string _id { get; set; }
		public string description { get; set; }
		public string assignedTo { get; set; }

		public RequestStatusType? GetStatus()
		{
			return status.GetStatus();
		}

		public static Request[][] GetRequestLists(Request[] requests, RequestStatusType[] Types = null)
		{
			if (requests == null || requests.Length <= 0) return null;
			var NewList = new List<Request>();
			var AssignedList = new List<Request>();
			var CancelledList = new List<Request>();
			var CompletedList = new List<Request>();
			foreach (Request request in requests) {
				switch (request.GetStatus()) {
					case RequestStatusType.New:
						NewList.Add(request);
						break;
					case RequestStatusType.Assigned:
						AssignedList.Add(request);
						break;
					case RequestStatusType.Completed:
						CompletedList.Add(request);
						break;
					case RequestStatusType.Cancelled:
						CancelledList.Add(request);
						break;
				}
			}
			if (Types != null) {
				var RequestLists = new List<List<Request>>();
				foreach (RequestStatusType type in Types) {
					switch (type) {
						case RequestStatusType.New:
							NewList.Reverse();
							RequestLists.Add(NewList);
							break;
						case RequestStatusType.Assigned:
							AssignedList.Reverse();
							RequestLists.Add(AssignedList);
							break;
						case RequestStatusType.Completed:
							CompletedList.Reverse();
							RequestLists.Add(CompletedList);
							break;
						case RequestStatusType.Cancelled:
							CancelledList.Reverse();
							RequestLists.Add(CancelledList);
							break;
					}
				}
				return RequestLists.Select(list => list.ToArray()).ToArray();
			}

			return new Request[][] { NewList.ToArray(), AssignedList.ToArray(), CompletedList.ToArray(), CancelledList.ToArray() };
		}
	}
}
