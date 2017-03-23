using Foundation;
using System;
using UIKit;
using Commercially.iOS.Extensions;

namespace Commercially.iOS {
	public abstract class AbstractField : UITextField {
		AbstractField _NextField;
		public AbstractField NextField {
			set {
				_NextField = value;
			}
		}
		public abstract UITextFieldDelegate FieldDelegate { get; }

		public AbstractField(IntPtr handle) : base(handle) { }

		public override void AwakeFromNib() {
			base.AwakeFromNib();
			Delegate = FieldDelegate;
			this.SetPlaceholderColor(this.TextColor);
		}

		public virtual bool IsValidInput() {
			return !string.IsNullOrWhiteSpace(Text);
		}

		public override UIReturnKeyType ReturnKeyType {
			get {
				return _NextField == null ? UIReturnKeyType.Done : UIReturnKeyType.Continue;
			}
		}

		public override UITextSpellCheckingType SpellCheckingType {
			get {
				return UITextSpellCheckingType.No;
			}
		}

		public override UITextAutocorrectionType AutocorrectionType {
			get {
				return UITextAutocorrectionType.No;
			}
		}

		public virtual void SetNextField(AbstractField nextResponder) {
			_NextField = nextResponder;
			Delegate = new AbstractFieldDelegate(this, nextResponder);
		}
	}
}