using System;
using System.Threading.Tasks;
using UIKit;
using Commercially.iOS.Extensions;

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
			NavigationItem.SetTitleImage("Logo-Red", NavigationController.NavigationBar);
			NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIImage.FromBundle("notification"), UIBarButtonItemStyle.Plain, (sender, e) => { });
			NavigationItem.RightBarButtonItem.TintColor = GlobalConstants.DefaultColors.Red.GetUIColor();

			SessionData.TaskFactory.StartNew(delegate {
				try {
					if (SessionData.TestMode) {
						SessionData.Requests = RequestApi.GetOfflineRequests();
						return;
					}
					SessionData.Requests = RequestApi.GetRequests();
				} catch (Exception e) {
					InvokeOnMainThread(delegate {
						Console.WriteLine(e.Message);
						NavigationController.ShowPrompt(e.Message, 50);
					});
				}
			});
		}
	}
}