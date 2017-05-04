// Created by Philip Gai

using System;
using UIKit;

namespace Commercially.iOS
{
	public class UserRolePickerModel : UIPickerViewModel
	{
		public override nint GetComponentCount(UIPickerView pickerView)
		{
			return 1;
		}

		public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
		{
			return UserRolePickerManager.Roles.Length;
		}

		public override string GetTitle(UIPickerView pickerView, nint row, nint component)
		{
			return UserRolePickerManager.Roles[row];
		}
	}
}
