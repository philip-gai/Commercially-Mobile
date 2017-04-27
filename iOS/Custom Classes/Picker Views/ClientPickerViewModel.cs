using System;
using UIKit;

namespace Commercially.iOS
{
	public class ClientPickerViewModel : UIPickerViewModel
	{
		readonly string[] DiscoveredBy;
		readonly Action<UIPickerView, nint, nint> OnSelect;

		public ClientPickerViewModel(FlicButton button, Action<UIPickerView, nint, nint> OnSelect)
		{
			DiscoveredBy = ButtonDetails.GetPickerOptions(button);
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
			return DiscoveredBy[row];
		}

		public override void Selected(UIPickerView pickerView, nint row, nint component)
		{
			OnSelect(pickerView, row, component);
		}
	}
}
