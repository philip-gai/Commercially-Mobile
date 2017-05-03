using System;
using Foundation;
using UIKit;

namespace Commercially.iOS
{
	public static class UITextFieldExtensions
	{
		public static void SetPlaceholderColor(this UITextField field, UIColor newColor)
		{
			field.AttributedPlaceholder = new NSAttributedString(field.Placeholder, foregroundColor: newColor);
		}

		public static void DisguiseAsTextView(this UITextField field)
		{
			field.Enabled = false;
			field.BorderStyle = UITextBorderStyle.None;
		}

		public static void ResignOnReturn(this UITextField field)
		{
			field.ShouldReturn += (textField) => { textField.ResignFirstResponder(); return true; };
		}
	}
}
