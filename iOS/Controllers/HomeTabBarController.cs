using System;
using System.Collections.Generic;
using UIKit;

namespace Commercially.iOS
{
	public partial class HomeTabBarController : UITabBarController
	{
		public HomeTabBarController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			NavigationItem.HidesBackButton = true;
			NavigationItem.SetTitleImage("Logo-Red-Toolbar");
			SetTabsForUser();
		}

		void SetTabsForUser()
		{
			if (Session.User.Type != UserRoleType.Admin) {
				var list = new List<UIViewController>(ViewControllers);
				int count = 0;
				for (int i = 0; i < ViewControllers.Length; i++) {
					var controller = ViewControllers[i];
					if (controller is ButtonListController || controller is UserListController) {
						list.RemoveAt(i-count);
						count++;
					}
				}
				SetViewControllers(list.ToArray(), true);
			}
		}
	}
}