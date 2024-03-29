// Created by Philip Gai

using System;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// Login controller.
	/// </summary>
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
				try {
					var response = UserApi.LoginUser(EmailField.Text, PasswordField.Text);
					Session.OAuth = new OAuthResponse(response);
					Session.User = UserApi.GetCurrentUser();
					NavigationController.GetAndActOnViewController(GlobalConstants.Screens.Home);
				} catch (Exception) {
					NavigationController.ShowPrompt(Localizable.PromptMessages.LoginError);
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