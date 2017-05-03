using System;
using UIKit;

namespace Commercially.iOS
{
	public class StatusPickerModel : UIPickerViewModel
	{
		readonly Action<UIPickerView, nint, nint> OnSelect;

		public StatusPickerModel(Action<UIPickerView, nint, nint> OnSelect)
		{
			this.OnSelect = OnSelect;
		}

		public override nint GetComponentCount(UIPickerView pickerView)
		{
			return 1;
		}

		public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
		{
			return StatusPicker.Statuses.Length;
		}

		public override string GetTitle(UIPickerView pickerView, nint row, nint component)
		{
			return StatusPicker.Statuses[row];
		}

		public override void Selected(UIPickerView pickerView, nint row, nint component)
		{
			OnSelect(pickerView, row, component);
		}
	}
}
