using System;
using UIKit;

namespace Commercially.iOS
{
	public static class LocalValidator
	{
		public static bool PasswordFieldsMatch(UITextField field1, UITextField field2)
		{
			return !field1.Text.Equals(field2.Text);
		}
		public static bool PasswordFieldsMatch(UnderlineField field1, UnderlineField field2)
		{
			if (!field1.Text.Equals(field2.Text)) {
				field1.SetLineColor(false);
				field2.SetLineColor(false);
				return false;
			}
			return true;
		}
	}
}
