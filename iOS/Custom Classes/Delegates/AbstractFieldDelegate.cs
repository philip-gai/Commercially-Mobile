using System;
using UIKit;

namespace Commercially.iOS {
	public class AbstractFieldDelegate : UITextFieldDelegate {
		protected AbstractField Field;
		protected AbstractField NextField;

		public AbstractFieldDelegate(IntPtr handle) : base(handle) { }

		public AbstractFieldDelegate(AbstractField field) {
			Field = field;
		}

		public AbstractFieldDelegate(AbstractField field, AbstractField nextField) {
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
	}
}
