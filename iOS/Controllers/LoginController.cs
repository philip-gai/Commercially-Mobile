using Foundation;
using System;
using UIKit;
using Commercially.iOS.Extensions;

namespace Commercially.iOS {
	public partial class LoginController : FieldListController {
		public LoginController(IntPtr handle) : base(handle) { }

		public override UIView[] FieldList {
			get {
				return new UIView[] { EmailField, PasswordField };
			}
		}

		public override UIButton Button {
			get {
				return LoginButton;
			}
		}

		public override UIScrollView ScrollView {
			get {
				return KeyboardScrollView;
			}
		}
		public override UIView[] UnderlineViews {
			get {
				return null;
			}
		}

		public override UIView ViewForUnderlines {
			get {
				return null;
			}
		}

		public override bool ShowNavigationBar {
			get {
				return true;
			}
		}

		public override void ButtonTouchUpInside(object sender, EventArgs events) {
			if (CheckIfFieldsValid()) {
				// Check if user exists in DB
				// Grab user information and cache / store in Session Data
			} else {
				if (!EmailField.IsValidInput()) {
					NavigationController.ShowPrompt(GlobalConstants.Prompts.InvalidEmail);
				} else if (!PasswordField.IsValidInput()) { 
					NavigationController.ShowPrompt(GlobalConstants.Prompts.InvalidPassword);
				}
			}
		}
	}
}