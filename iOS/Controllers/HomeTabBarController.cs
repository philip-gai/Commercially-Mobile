// Created by Philip Gai

using System;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// Home tab bar controller.
	/// </summary>
	public partial class HomeTabBarController : UITabBarController
	{
		public HomeTabBarController(IntPtr handle) : base(handle) { }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			NavigationItem.HidesBackButton = true;
			NavigationItem.SetTitleImage("Logo-Red-Toolbar");
			// Hide certain tabs from non-admins
			if (Session.User.Type != UserRoleType.Admin) {
				ViewControllers = new UIViewController[] { UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.RequestList), UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.UserDetails) };
			}
		}
	}
}