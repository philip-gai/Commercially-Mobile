using System;
using UIKit;
namespace Commercially.iOS.Extensions
{
	public static class UIPickerViewExtensions
	{
		public static void ScrollToTitle(this UIPickerView pickerView, string title, int component = 0)
		{
			for (int row = 0; row < pickerView.Model.GetRowsInComponent(pickerView, component); row++) {
				if (pickerView.Model.GetTitle(pickerView, row, component).Equals(title)) {
					pickerView.Select(row, component, true);
					break;
				}
			}
		}

		public static bool IsTitleMatch(this UIPickerView pickerView, string title, nint row, nint component)
		{
			return pickerView.Model.GetTitle(pickerView, row, component).Equals(title);
		}
	}
}
