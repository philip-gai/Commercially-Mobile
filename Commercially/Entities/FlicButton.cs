using System;
namespace Commercially
{
	[Serializable]
	public class FlicButton
	{
		public string description { get; set; }
		public string bluetooth_id { get; set; }
		public string room { get; set; }
		public string clientId { get; set; }
		public string[] discoveredBy { get; set; }
	}
}
