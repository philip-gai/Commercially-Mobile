using System;
namespace Commercially
{
	public class Request
	{
		public Request() { }
		public Request(string Room, FlicButton Button) {
			this.Room = Room;
			this.Button = Button;
			Received = DateTime.Now;
			Status = Status.ToDo;
			Urgent = false;
		}
		public Request(string Room, FlicButton Button, bool Urgent): this(Room, Button) {
			this.Urgent = Urgent;
		}
		public string Room { get; set; }
		public DateTime Received { get; set; }
		public DateTime Complete { get; set; }
		public DateTime InProgress { get; set; }
		public Status Status { get; set; }
		public FlicButton Button { get; set; }
		public bool Urgent { get; set; }

		static int num = 1;
		static Random rand = new Random();
		public static Request GetDummyRequest() {
			var DummyReq = new Request();
			DummyReq.Room = "Room " + num;
			DummyReq.Button = new FlicButton("Request for X", num++.ToString());
			DummyReq.Received = DateTime.Now;
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
			DummyReq.Urgent = rand.Next() % 2 == 0;
			return DummyReq;
		}
	}
}
