using System;
using UIKit;

namespace Commercially.iOS
{
	public class ClientPickerViewModel : UIPickerViewModel
	{
		readonly Client[] DiscoveredBy;
		readonly Action<UIPickerView, nint, nint> OnSelect;

		public ClientPickerViewModel(Action<UIPickerView, nint, nint> OnSelect)
		{
			this.OnSelect = OnSelect;
		}

		public override nint GetComponentCount(UIPickerView pickerView)
		{
			return 1;
		}

		public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
		{
			return DiscoveredBy.Length;
		}

		public override string GetTitle(UIPickerView pickerView, nint row, nint component)
		{
			return DiscoveredBy[row].friendlyName;
		}

		public override void Selected(UIPickerView pickerView, nint row, nint component)
		{
			OnSelect(pickerView, row, component);
		}
	}
}
