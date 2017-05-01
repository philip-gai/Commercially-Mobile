using System;
using System.Collections.Generic;
using UIKit;

namespace Commercially.iOS
{
	public class UserPickerViewModel : UIPickerViewModel
	{
		readonly static string[] Users = GetUsernames();
		readonly Action<UIPickerView, nint, nint> OnSelect;

		public UserPickerViewModel(Action<UIPickerView, nint, nint> OnSelect)
		{
			this.OnSelect = OnSelect;
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
			OnSelect(pickerView, row, component);
		}

		static string[] GetUsernames()
		{
			var users = UserApi.GetUsers();
			var usernameList = new List<string>();
			usernameList.Add(Localizable.Labels.NoneOption);
			foreach (var user in users) {
				usernameList.Add(user.username);
			}
			return usernameList.ToArray();
		}
	}
}
