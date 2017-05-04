// Created by Philip Gai

using System;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// UIPicker view extensions.
	/// </summary>
	public static class UIPickerViewExtensions
	{
		/// <summary>
		/// Scrolls to title.
		/// </summary>
		/// <param name="pickerView">Picker view.</param>
		/// <param name="title">Title.</param>
		/// <param name="component">Component.</param>
		public static void ScrollToTitle(this UIPickerView pickerView, string title, int component = 0)
		{
			for (int row = 0; row < pickerView.Model.GetRowsInComponent(pickerView, component); row++) {
				if (pickerView.Model.GetTitle(pickerView, row, component).Equals(title, StringComparison.CurrentCultureIgnoreCase)) {
					pickerView.Select(row, component, true);
					break;
				}
			}
		}

		/// <summary>
		/// Gets if the title is a match.
		/// </summary>
		/// <returns><c>true</c>, if the title is a match, <c>false</c> otherwise.</returns>
		/// <param name="pickerView">Picker view.</param>
		/// <param name="title">Title.</param>
		/// <param name="row">Row.</param>
		/// <param name="component">Component.</param>
		public static bool IsTitleMatch(this UIPickerView pickerView, string title, nint row, nint component)
		{
			return pickerView.Model.GetTitle(pickerView, row, component).Equals(title, StringComparison.CurrentCultureIgnoreCase);
		}
	}
}
