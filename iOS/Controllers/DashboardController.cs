using Foundation;
using System;
using UIKit;
using Commercially.iOS.Extensions;

namespace Commercially.iOS
{
    public partial class DashboardController : UIViewController
    {
        public DashboardController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			NavigationItem.SetTitleImage("Logo-Red", NavigationController.NavigationBar);
		}
    }
}