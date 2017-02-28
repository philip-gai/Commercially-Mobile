using Foundation;
using System;
using UIKit;
using Commercially.iOS.Extensions;

namespace Commercially.iOS
{
	public partial class DashboardController : NavigationBarViewController
	{
		public DashboardController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			NavigationItem.LeftItemsSupplementBackButton = false;
			NavigationItem.HidesBackButton = true;
			NavigationItem.SetHidesBackButton(true, true);
			NavigationItem.SetTitleImage("Logo-Red", NavigationController.NavigationBar);
		}
	}
}