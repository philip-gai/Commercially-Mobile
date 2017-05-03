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
			return UserRolePicker.Roles.Length;
		}

		public override string GetTitle(UIPickerView pickerView, nint row, nint component)
		{
			return UserRolePicker.Roles[row];
		}
	}
}
