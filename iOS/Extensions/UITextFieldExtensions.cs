// Created by Philip Gai

using Foundation;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// UITextGield extensions.
	/// </summary>
	public static class UITextFieldExtensions
	{
		/// <summary>
		/// Sets the color of the placeholder.
		/// </summary>
		/// <param name="field">Field.</param>
		/// <param name="newColor">New color.</param>
		public static void SetPlaceholderColor(this UITextField field, UIColor newColor)
		{
			field.AttributedPlaceholder = new NSAttributedString(field.Placeholder, foregroundColor: newColor);
		}

		/// <summary>
		/// Disguises as text view.
		/// </summary>
		/// <param name="field">Field.</param>
		public static void DisguiseAsTextView(this UITextField field)
		{
			field.Enabled = false;
			field.BorderStyle = UITextBorderStyle.None;
		}

		/// <summary>
		/// Sets should return to resign first responder.
		/// </summary>
		/// <param name="field">Field.</param>
		public static void ResignOnReturn(this UITextField field)
		{
			field.ShouldReturn += (textField) => { textField.ResignFirstResponder(); return true; };
		}
	}
}
