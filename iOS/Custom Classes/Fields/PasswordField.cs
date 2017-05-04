// Created by Philip Gai

using System;

namespace Commercially.iOS
{
	/// <summary>
	/// Password field.
	/// </summary>
	public partial class PasswordField : UnderlineField
	{
		public PasswordField(IntPtr handle) : base(handle) { }

		public override bool IsValidInput()
		{
			return Validator.Password(Text);
		}

		public override bool SecureTextEntry {
			get {
				return true;
			}
		}
	}
}