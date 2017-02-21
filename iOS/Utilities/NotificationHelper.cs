using System;
using Foundation;

namespace Commercially.iOS {
	public static class NotificationHelper {
		public static NSObject ObserveNotification(string NotificationName, Action<NSNotification> action) {
			return NSNotificationCenter.DefaultCenter.AddObserver(new NSString(NotificationName), action);
		}

		public static void PushViewController(string storyboardName) {
			NSNotificationCenter.DefaultCenter.PostNotificationName(LocalConstants.Notifications.PushViewController.Name, null, new NSDictionary(LocalConstants.Notifications.PushViewController.UserInfo, storyboardName));
		}

		public static void PopViewController(NSDictionary userInfo = null) {
			NSNotificationCenter.DefaultCenter.PostNotificationName(LocalConstants.Notifications.PopViewController, null, userInfo);
		}
	}
}
