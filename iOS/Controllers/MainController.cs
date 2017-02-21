using Foundation;
using System;
using UIKit;
using Commercially.iOS.Extensions;

namespace Commercially.iOS {
	public partial class MainController : UINavigationController {
		NSObject pushToken, popToken, hidePromptToken;

		public MainController(IntPtr handle) : base(handle) { }

		public override void ViewDidLoad() {
			base.ViewDidLoad();
			pushToken = NotificationHelper.ObserveNotification(LocalConstants.Notifications.PushViewController.Name, HandlePushViewController);
			popToken = NotificationHelper.ObserveNotification(LocalConstants.Notifications.PopViewController, HandlePopViewController);
			hidePromptToken = NotificationHelper.ObserveNotification(LocalConstants.Notifications.HidePrompt, HandleHidePrompt);
			this.GetAndActOnViewController(GlobalConstants.Screens.Login);
		}

		public override void ViewDidUnload() {
			base.ViewDidUnload();
			pushToken.Dispose();
			popToken.Dispose();
			hidePromptToken.Dispose();
		}

		void HandlePushViewController(NSNotification notification) {
			this.GetAndActOnViewController(notification);
		}

		void HandlePopViewController(NSNotification notification) {
			PopViewController(true);
		}

		void HandleHidePrompt(NSNotification notification) {
			View.BackgroundColor = UIColor.Clear;
			View.Alpha = (nfloat)1.0;
		}
	}
}