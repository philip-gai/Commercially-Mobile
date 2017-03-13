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
				return true;
			}
		}

		public override UIColor UnderlineColor {
			get {
				return GlobalConstants.DefaultColors.Red.GetUIColor().ColorWithAlpha((nfloat)0.25);
			}
		}

		public override void ButtonTouchUpInside(object sender, EventArgs events)
		{
			// MUST REMOVE THIS LATER. FOR TESTING ONLY
			NavigationController.GetAndActOnViewController(GlobalConstants.Screens.Home);
			if (CheckIfFieldsValid()) {
				try {
					var response = UserApi.LoginUser(EmailField.Text, PasswordField.Text);
					SessionData.OAuth = new OAuthResponse(response);
					NavigationController.GetAndActOnViewController(GlobalConstants.Screens.Home);
				} catch (Exception e) {
					NavigationController.ShowPrompt(e.Message.Substring(0, 20) + "...");
				}
				// Check if user exists in DB
				// Grab user information and cache / store in Session Dat
			} else {
				if (!EmailField.IsValidInput()) {
					NavigationController.ShowPrompt(Localizable.PromptMessages.InvalidEmail);
				} else if (!PasswordField.IsValidInput()) {
					NavigationController.ShowPrompt(Localizable.PromptMessages.InvalidPassword);
				}
			}
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			NavigationController.SetNavigationBarHidden(true, false);
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			NavigationController.SetNavigationBarHidden(false, false);
		}
	}
}