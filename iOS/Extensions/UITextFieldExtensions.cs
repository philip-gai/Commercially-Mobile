using System;
using Foundation;
using UIKit;

namespace Commercially.iOS.Extensions
{
	public static class UITextFieldExtensions
	{
		public static void SetPlaceholderColor(this UITextField field, UIColor newColor)
		{
			field.AttributedPlaceholder = new NSAttributedString(field.Placeholder, foregroundColor: newColor);
		}
	}
}
