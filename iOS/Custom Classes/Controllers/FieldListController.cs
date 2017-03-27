using System;
using UIKit;
using Commercially.iOS.Extensions;
using Foundation;

namespace Commercially.iOS {
	public abstract class FieldListController : UnderlineViewController {
		public abstract UIView[] FieldList { get; }
		NSObject keyboardShowToken, keyboardHideToken;

		public abstract UIButton Button { get; }
		public abstract UIScrollView ScrollView { get; }

		public abstract void ButtonTouchUpInside(object sender, EventArgs events);

		public FieldListController(IntPtr handle) : base(handle) { }

		public override bool AutomaticallyAdjustsScrollViewInsets {
			get {
				return false;
			}
		}

		public override void ViewDidLoad() {
			base.ViewDidLoad();
			View.HideKeyboardWhenTapped();
			SetNextResponders();
			Button.TouchUpInside += ButtonTouchUpInside;
			Button.TouchUpInside += HandleKeyboardOnButtonTouch;
			ScrollView.Bounces = false;
		}

		public override void ViewDidUnload() {
			base.ViewDidUnload();
			Button.TouchUpInside -= ButtonTouchUpInside;
			Button.TouchUpInside -= HandleKeyboardOnButtonTouch;
		}

		public override void ViewWillAppear(bool animated) {
			base.ViewWillAppear(animated);
			keyboardShowToken = NotificationHelper.ObserveNotification(UIKeyboard.DidShowNotification, HandleKeyboardDidShow);
			keyboardHideToken = NotificationHelper.ObserveNotification(UIKeyboard.DidHideNotification, HandleKeyboardDidHide);
		}

		public override void ViewWillDisappear(bool animated) {
			base.ViewWillDisappear(animated);
			View.EndEditing(true);
			keyboardShowToken.Dispose();
			keyboardHideToken.Dispose();
		}

		public override void ViewDidDisappear(bool animated) {
			base.ViewDidDisappear(animated);
		}

		void HandleKeyboardDidShow(NSNotification notification) {
			this.KeyboardDid(UIViewControllerExtensions.KeyboardActionType.Show, notification);
		}

		void HandleKeyboardDidHide(NSNotification notification) {
			this.KeyboardDid(UIViewControllerExtensions.KeyboardActionType.Hide, notification);
		}

		void HandleKeyboardOnButtonTouch(object sender, EventArgs events) {
			View.EndEditing(true);
		}

		public bool CheckIfFieldsValid() {
			bool fieldValid = true;
			foreach (UIView view in FieldList) {
				if (view is AbstractField) {
					var textField = view as AbstractField;
					if (fieldValid) {
						fieldValid = textField.IsValidInput();
					}
					if (textField is UnderlineField) {
						(textField as UnderlineField).SetLineColor(textField.IsValidInput());
					}
				} else if (view is UnderlineButton) {
					var button = view as UnderlineButton;
					if (fieldValid) {
						fieldValid = button.IsValid();
					}
					button.SetLineColor(button.IsValid());
				}
			}
			return fieldValid;
		}

		public void ResetFields() {
			foreach (UIView view in FieldList) {
				if (view is AbstractField) {
					var field = view as AbstractField;
					field.Text = "";
					if (field is UnderlineField) {
						(field as UnderlineField).SetLineColor(true);
					}
				} else if (view is UnderlineButton) {
					var button = view as UnderlineButton;
					button.SetTitle(button.OriginalText, UIControlState.Normal);
					button.SetLineColor(true);
				}
			}
		}

		void SetNextResponders() {
			AbstractField previousField = null;
			foreach (UIView view in FieldList) {
				if (view is AbstractField) {
					var field = view as AbstractField;
					if (previousField != null) {
						previousField.SetNextField(field);
					}
					previousField = field;
				}
			}
		}
	}
}
