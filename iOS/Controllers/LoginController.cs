using System;
using UIKit;
using Commercially.iOS.Extensions;

namespace Commercially.iOS
{
	public partial class LoginController : FieldListController
	{
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
				return new UIView[] { EmailField, PasswordField };
			}
		}

		public override UIView ViewForUnderlines {
			get {
				return KeyboardScrollView;
			}
		}

		public override bool ShowNavigationBar {
			get {
				return false;
			}
		}

		public override UIColor UnderlineColor {
			get {
				return GlobalConstants.DefaultColors.Red.GetUIColor().ColorWithAlpha((nfloat)0.25);
			}
		}

		public override void ButtonTouchUpInside(object sender, EventArgs events)
		{
			if (CheckIfFieldsValid()) {
				if (SessionData.TestMode) {
					// MUST REMOVE THIS LATER. FOR TESTING ONLY
					NavigationController.GetAndActOnViewController(GlobalConstants.Screens.Home);
					return;
				}
				try {
					var response = UserApi.LoginUser(EmailField.Text, PasswordField.Text);
					SessionData.OAuth = new OAuthResponse(response);
					SessionData.User = new User(EmailField.Text, PasswordField.Text);
					NavigationController.GetAndActOnViewController(GlobalConstants.Screens.Home);
				} catch (Exception e) {
					NavigationController.ShowPrompt(e.Message, 50);
				}
			} else {
				if (!EmailField.IsValidInput()) {
					NavigationController.ShowPrompt(Localizable.PromptMessages.InvalidEmail);
				} else if (!PasswordField.IsValidInput()) {
					NavigationController.ShowPrompt(Localizable.PromptMessages.InvalidPassword);
				}
			}
		}
	}
}