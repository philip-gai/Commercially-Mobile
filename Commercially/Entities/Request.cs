using System;
namespace Commercially
{
	public class Request
	{
		public Request() { }
		public Request(string Room)
		{
			this.room = Room;
			//this.button_id = Button;
			time_received = DateTime.Now;
			Status = Status.ToDo;
			urgent = false;
		}

		public string room { get; set; }
		public DateTime time_received { get; set; }
		public DateTime time_completed { get; set; }
		public DateTime time_scheduled { get; set; }
		public Status Status { get; set; }
		public string button_id { get; set; }
		public bool urgent { get; set; }
		public string _id { get; set; }
		public string description { get; set; }
		public string assignedTo { get; set; }

		static int num = 1;
		static Random rand = new Random();
		public static Request GetDummyRequest()
		{
			var DummyReq = new Request();
			DummyReq.room = "Room " + num;
			DummyReq.button_id = num++.ToString();
			DummyReq.time_received = DateTime.Now;
			Status Status;
			switch (rand.Next() % 3) {
				case 0:
					Status = Status.ToDo;
					break;
				case 1:
					Status = Status.InProgress;
					break;
				case 2:
					Status = Status.Complete;
					break;
				default:
					Status = Status.ToDo;
					break;
			}
			DummyReq.Status = Status;
			DummyReq.urgent = rand.Next() % 2 == 0;
			return DummyReq;
		}
	}
}
