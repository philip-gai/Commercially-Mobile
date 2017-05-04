// Created by Philip Gai

using System;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// User picker view model.
	/// </summary>
	public class UserPickerViewModel : UIPickerViewModel
	{
		readonly string[] Users;
		readonly Action<UIPickerView, nint, nint> PickerSelected;

		public UserPickerViewModel(Action<UIPickerView, nint, nint> PickerSelected)
		{
			Users = RequestDetailsManager.GetUserPickerOptions();
			this.PickerSelected = PickerSelected;
		}

		public override nint GetComponentCount(UIPickerView pickerView)
		{
			return 1;
		}

		public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
		{
			return Users.Length;
		}

		public override string GetTitle(UIPickerView pickerView, nint row, nint component)
		{
			return Users[row];
		}

		public override void Selected(UIPickerView pickerView, nint row, nint component)
		{
			PickerSelected(pickerView, row, component);
		}
	}
}
