using System;
using UIKit;

namespace Commercially.iOS {
	public class UnderlineFieldDelegate : UITextFieldDelegate {
		protected UnderlineField Field;
		protected UnderlineField NextField;

		public UnderlineFieldDelegate(IntPtr handle) : base(handle) { }

		public UnderlineFieldDelegate(UnderlineField field) {
			Field = field;
		}

		public UnderlineFieldDelegate(UnderlineField field, UnderlineField nextField) {
			Field = field;
			NextField = nextField;
		}

		public override bool ShouldReturn(UITextField textField) {
			if (textField == Field) {
				textField.ResignFirstResponder();
				if (NextField != null) {
					NextField.BecomeFirstResponder();
				}
			}
			return true;
		}

		public override void EditingEnded(UITextField textField) {
			UnderlineField UnderlineField = Field is UnderlineField ? Field as UnderlineField : null;
			if (UnderlineField == null) return;
			UnderlineField.SetLineColor(UnderlineField.IsValidInput());
		}
	}
}
