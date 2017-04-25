using System;
using UIKit;
using Commercially.iOS.Extensions;
using System.Collections.Generic;

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
			Home.PrefetchData();
			SetTabsForUser();
		}

		void SetTabsForUser()
		{
			if (Session.User.GetUserRoleType() != UserRoleType.Admin) {
				var list = new List<UIViewController>(ViewControllers);
				for (int i = 0; i < ViewControllers.Length; i++) {
					var controller = ViewControllers[i];
					if (controller is ButtonListController) {
						list.RemoveAt(i);
					}
				}
				SetViewControllers(list.ToArray(), true);
			}
		}
	}
}