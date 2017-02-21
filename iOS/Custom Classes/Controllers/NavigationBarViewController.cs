using System;
using UIKit;

namespace Commercially.iOS {
	public class NavigationBarViewController : UIViewController {
		public NavigationBarViewController(IntPtr handle) : base(handle) { }

		public override void ViewWillAppear(bool animated) {
			base.ViewWillAppear(animated);
			NavigationController.SetNavigationBarHidden(false, false);
		}

		public override void ViewWillDisappear(bool animated) {
			base.ViewWillDisappear(animated);
			NavigationController.SetNavigationBarHidden(true, false);
		}
	}
}