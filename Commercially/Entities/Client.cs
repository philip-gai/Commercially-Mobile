using System;

namespace Commercially
{
	[Serializable]
	public class Client
	{
		public string clientId { get; set; }
		public string friendlyName { get; set; }
		public bool authorized { get; set; }
	}
}