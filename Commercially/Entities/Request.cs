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

		public string GetTime(TimeType type)
		{
			DateTime? Time = null;
			switch (type) {
				case TimeType.Received:
					Time = time_received.ConvertToDateTime();
					break;
				case TimeType.Scheduled:
					Time = time_scheduled.ConvertToDateTime();
					break;
				case TimeType.Completed:
					Time = time_completed.ConvertToDateTime();
					break;
			}
			if (Time == null) {
				return null;
			}
			return Time?.ToShortTimeString() + " " + Time?.ToShortDateString();
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
							RequestLists.Add(NewList);
							break;
						case RequestStatusType.Assigned:
							RequestLists.Add(AssignedList);
							break;
						case RequestStatusType.Completed:
							RequestLists.Add(CompletedList);
							break;
						case RequestStatusType.Cancelled:
							RequestLists.Add(CancelledList);
							break;
					}
				}
				return RequestLists.Select(list => list.ToArray()).ToArray();
			}

			return new Request[][] { NewList.ToArray(), AssignedList.ToArray(), CompletedList.ToArray(), CancelledList.ToArray() };
		}

		static int num = 1;
		readonly static Random rand = new Random();
		public static Request GetDummyRequest()
		{
			var DummyReq = new Request();
			DummyReq._id = Guid.NewGuid().ToString();
			DummyReq.description = "Replace the Toilet Paper";
			DummyReq.time_received = DateTime.Now.ConvertToMilliseconds().ToString();
			DummyReq.room = "Room " + num++;
			DummyReq.button_id = Guid.NewGuid().ToString();
			string status;
			switch (rand.Next() % 4) {
				case 0:
					status = RequestStatusType.New.ToString().ToLower();
					break;
				case 1:
					status = RequestStatusType.Assigned.ToString().ToLower();
					break;
				case 2:
					status = RequestStatusType.Completed.ToString().ToLower();
					break;
				case 3:
					status = RequestStatusType.Cancelled.ToString().ToLower();
					break;
				default:
					status = null;
					break;
			}
			DummyReq.status = status;
			DummyReq.urgent = rand.Next() % 2 == 0;
			return DummyReq;
		}
	}
}
