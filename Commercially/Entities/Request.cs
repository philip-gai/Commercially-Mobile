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
		}
		public string Room { get; set; }
		public DateTime Received { get; set; }
		public DateTime Complete { get; set; }
		public DateTime InProgress { get; set; }
		public Status Status { get; set; }
		public FlicButton Button { get; set; }
	}
}
