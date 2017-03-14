using System;
using UIKit;
namespace Commercially.iOS
{
	public class PickerDelegate : UIPickerViewDelegate
	{
		readonly Action<UIPickerView, nint, nint> OnSelect;
		public PickerDelegate(Action<UIPickerView, nint, nint> OnSelect)
		{
			this.OnSelect = OnSelect;
		}

		public override void Selected(UIPickerView pickerView, nint row, nint component)
		{
			OnSelect(pickerView, row, component);
		}
	}
}
