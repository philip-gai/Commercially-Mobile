// Created by Philip Gai

using System;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// Client picker view model.
	/// </summary>
	public class ClientPickerViewModel : UIPickerViewModel
	{
		readonly string[] DiscoveredBy;
		readonly Action<UIPickerView, nint, nint> PickerSelected;

		public ClientPickerViewModel(FlicButton button, Action<UIPickerView, nint, nint> PickerSelected)
		{
			DiscoveredBy = ButtonDetailsManager.GetPickerOptions(button);
			this.PickerSelected = PickerSelected;
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
			PickerSelected(pickerView, row, component);
		}
	}
}
