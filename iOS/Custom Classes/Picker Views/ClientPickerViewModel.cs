﻿using System;
using UIKit;

namespace Commercially.iOS
{
	public class ClientPickerViewModel : UIPickerViewModel
	{
		public const string Placeholder = "-- None --";
		readonly Client[] DiscoveredBy;
		readonly Action<UIPickerView, nint, nint> OnSelect;

		public ClientPickerViewModel(Client[] DiscoveredBy, Action<UIPickerView, nint, nint> OnSelect)
		{
			this.DiscoveredBy = DiscoveredBy;
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
			if (row == 0) {
				return Placeholder;
			}
			return DiscoveredBy[row-1].friendlyName;
		}

		public override void Selected(UIPickerView pickerView, nint row, nint component)
		{
			OnSelect(pickerView, row, component);
		}
	}
}
