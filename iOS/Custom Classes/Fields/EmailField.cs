// Created by Philip Gai

using System;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// Email field.
	/// </summary>
	public partial class EmailField : UnderlineField
	{
		public EmailField(IntPtr handle) : base(handle) { }

		public override bool IsValidInput()
		{
			return Validator.Email(Text);
		}

		public override UIKeyboardType KeyboardType {
			get {
				return UIKeyboardType.EmailAddress;
			}
		}
	}
}