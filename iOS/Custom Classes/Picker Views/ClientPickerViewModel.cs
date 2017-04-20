using System;
using UIKit;

namespace Commercially.iOS
{
	public class ClientPickerViewModel : UIPickerViewModel
	{
		readonly Client[] DiscoveredBy;
		readonly Action<UIPickerView, nint, nint> OnSelect;

		public ClientPickerViewModel(Client[] DiscoveredBy, Action<UIPickerView, nint, nint> OnSelect)
		{
			Client[] tmpArr = new Client[DiscoveredBy.Length + 1];
			DiscoveredBy.CopyTo(tmpArr, 1);
			tmpArr[0] = Client.FindClient("ignore", Session.Clients);
			this.DiscoveredBy = tmpArr;
			this.OnSelect = OnSelect;
		}

		public override nint GetComponentCount(UIPickerView pickerView)
		{
			return 1;
		}

		public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
		{
			return DiscoveredBy != null ? DiscoveredBy.Length+1 : 1;
		}

		public override string GetTitle(UIPickerView pickerView, nint row, nint component)
		{
			if (row == 0) {
				return Localizable.Labels.NoneOption;
			}
			Client client = DiscoveredBy[row - 1];
			return string.IsNullOrWhiteSpace(client.friendlyName) ? client.clientId : client.friendlyName;
		}

		public override void Selected(UIPickerView pickerView, nint row, nint component)
		{
			OnSelect(pickerView, row, component);
		}
	}
}
