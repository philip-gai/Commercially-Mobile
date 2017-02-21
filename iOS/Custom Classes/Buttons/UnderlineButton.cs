using Foundation;
using System;
using UIKit;

namespace Commercially.iOS {
	public partial class UnderlineButton : UIButton {
		UIView UnderlineView;
		string _OriginalText;
		public string OriginalText {
			get {
				return _OriginalText;
			}
		}

		public UnderlineButton(IntPtr handle) : base(handle) { }

		public override void AwakeFromNib() {
			base.AwakeFromNib();
			_OriginalText = TitleLabel.Text;
		}

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
			if (valid) {
				UnderlineView.BackgroundColor = LocalConstants.LineColor;
			} else {
				UnderlineView.BackgroundColor = LocalConstants.LineIncompleteColor;
			}
		}

		public virtual bool IsValid() {
			return !TitleLabel.Text.Equals(OriginalText);
		}
	}
}