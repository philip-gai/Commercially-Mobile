using System;
namespace Commercially
{
	public class Request
	{
		public Request(string Room, FlicButton Button) {
			this.Room = Room;
			this.Button = Button;
			this.Received = DateTime.Now;
			this.Status = Status.ToDo;
			this.Urgent = false;
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
	}
}
