using System;
namespace Commercially
{
	public class FlicButton
	{
		public FlicButton(string BluetoothId) {
			this.BluetoothId = BluetoothId;
		}
		public FlicButton(string Message, string BluetoothId)
		{
			this.Message = Message;
			this.BluetoothId = BluetoothId;
		}
		public string Message { get; set; }
		public string BluetoothId { get; set; }
	}
}
