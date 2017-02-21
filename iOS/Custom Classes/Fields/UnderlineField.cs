using System;
using UIKit;

namespace Commercially.iOS {
	public partial class UnderlineField : AbstractField {
		UIView UnderlineView;

		public UnderlineField(IntPtr handle) : base(handle) { }

		public void SetUnderlineView(UIView view) {
			UnderlineView = view;
		}

		public UIView GetUnderlineView() {
			return UnderlineView;
		}

		public void ClearUnderlineView() {
			UnderlineView = null;
		}

		public void SetLineColor(bool valid) {
			if (UnderlineView == null) return;
			if (valid) {
				UnderlineView.BackgroundColor = LocalConstants.LineColor;
			} else {
				UnderlineView.BackgroundColor = LocalConstants.LineIncompleteColor;
			}
		}

		public override void SetNextField(AbstractField nextResponder) {
			if (!(nextResponder is UnderlineField)) return;
			NextField = nextResponder;
			Delegate = new UnderlineFieldDelegate(this, (nextResponder as UnderlineField));
		}

		public override UITextFieldDelegate FieldDelegate {
			get {
				return new UnderlineFieldDelegate(this);
			}
		}
	}
}
