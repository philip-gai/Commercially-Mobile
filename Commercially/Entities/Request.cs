using System;
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

		public Status GetStatus()
		{
			const string NewStr = "new";
			const string assigned = "assigned";
			const string completed = "completed";
			const string cancelled = "cancelled";

			if (string.IsNullOrWhiteSpace(status)) {
				return Status.Undefined;
			}

			switch (status) {
				case NewStr:
					return Status.New;
				case assigned:
					return Status.Assigned;
				case completed:
					return Status.Completed;
				case cancelled:
					return Status.Cancelled;
				default:
					return Status.Undefined;
			}
		}

		static int num = 1;
		static Random rand = new Random();
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
					status = Status.New.ToString().ToLower();
					break;
				case 1:
					status = Status.Assigned.ToString().ToLower();
					break;
				case 2:
					status = Status.Completed.ToString().ToLower();
					break;
				case 3:
					status = Status.Cancelled.ToString().ToLower();
					break;
				default:
					status = Status.Undefined.ToString().ToLower();
					break;
			}
			DummyReq.status = status;
			DummyReq.urgent = rand.Next() % 2 == 0;
			return DummyReq;
		}
	}
}
