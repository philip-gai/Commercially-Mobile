using System;
using Foundation;
using UIKit;
using CoreGraphics;

namespace Commercially.iOS.Extensions {
	public static class UINavigationControllerExtensions {
		public enum NavigationType { Push, Present };

		public static void GetAndActOnViewController(this UINavigationController nav, UIViewController nextController, NavigationType type = NavigationType.Push) {
			switch (type) {
				case NavigationType.Push:
					nav.PushViewController(nextController, true);
					break;
				case NavigationType.Present:
					nav.PresentViewControllerAsync(nextController, true);
					break;
			}
		}

		public static void GetAndActOnViewController(this UINavigationController nav, string storyboardName, NavigationType type = NavigationType.Push) {
			UIViewController nextController = GetViewController(storyboardName);
			nav.GetAndActOnViewController(nextController, type);
		}

		public static void GetAndActOnViewController(this UINavigationController nav, NSNotification notification, NavigationType type = NavigationType.Push) {
			string storyboardName = notification.UserInfo[LocalConstants.Notifications.PushViewController.UserInfo].ToString();
			nav.GetAndActOnViewController(storyboardName, type);
		}

		public static UIViewController GetViewController(string storyboardName) {
			UIStoryboard nextStoryboard = UIStoryboard.FromName(storyboardName, null);
			UIViewController nextController = nextStoryboard.InstantiateInitialViewController();
			return nextController;
		}

		public static void PopToViewController<T>(this UINavigationController nav) {
			int index = -1;
			int length = nav.ViewControllers.Length;
			for (int i = 0; i < nav.ViewControllers.Length; i++) {
				if (nav.ViewControllers[i] is T) {
					index = i;
					break;
				}
			}
			for (; index < length; index++) {
				nav.PopViewController(true);
			}
		}

		public static void ShowPrompt(this UINavigationController nav, string promptInfo) {
			var promptController = GetViewController(GlobalConstants.Screens.Prompt) as PromptController;
			promptController.SetPrompt(promptInfo);
			promptController.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
			nav.PresentModalViewController(promptController, true);
			nav.View.BackgroundColor = UIColor.Black;
			nav.View.Alpha = (nfloat)0.5;
		}

		public static void ShowPrompt(this UINavigationController nav, NSNotification notification) {
			string promptInfo = notification.UserInfo[LocalConstants.Notifications.ShowPrompt.UserInfo].ToString();
			nav.ShowPrompt(promptInfo);
		}
	}
}
