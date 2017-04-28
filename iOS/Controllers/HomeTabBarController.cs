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
			TabBar.Hidden = Session.User.Type != UserRoleType.Admin;
		}
	}
}