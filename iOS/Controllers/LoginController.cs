using Foundation;
using System;
using System.Net;
using UIKit;
using Commercially.iOS.Extensions;
using Newtonsoft.Json;

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

		public override void ButtonTouchUpInside(object sender, EventArgs events)
		{
			NavigationController.GetAndActOnViewController(GlobalConstants.Screens.Home);
			return;
			if (CheckIfFieldsValid()) {
				try {
					string response = HttpRequest.MakeRequest(HttpRequestMethodType.POST, "http://" + GlobalConstants.ServerUrl + ":" + GlobalConstants.ServerPort + "/user/token", "grant_type=password&username=" + WebUtility.UrlEncode(EmailField.Text) + "&password=" + WebUtility.UrlEncode(PasswordField.Text) + "&client_id=MobileApp&client_secret=null");
					SessionData.OAuth = new OAuthResponse(response);
					NavigationController.GetAndActOnViewController(GlobalConstants.Screens.Home);
				} catch (Exception e) {
					NavigationController.ShowPrompt(e.Message);
				}
				//UserApi.MakeUserRequest(HttpRequestMethodType.POST, ):
				// Check if user exists in DB
				// Grab user information and cache / store in Session Dat
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