// Created by Philip Gai

using System;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// Status picker model.
	/// </summary>
	public class StatusPickerModel : UIPickerViewModel
	{
		readonly Action<UIPickerView, nint, nint> PickerSelected;

		public StatusPickerModel(Action<UIPickerView, nint, nint> PickerSelected)
		{
			this.PickerSelected = PickerSelected;
		}

		public override nint GetComponentCount(UIPickerView pickerView)
		{
			return 1;
		}

		public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
		{
			return StatusPickerManager.Statuses.Length;
		}

		public override string GetTitle(UIPickerView pickerView, nint row, nint component)
		{
			return StatusPickerManager.Statuses[row];
		}

		public override void Selected(UIPickerView pickerView, nint row, nint component)
		{
			PickerSelected(pickerView, row, component);
		}
	}
}
