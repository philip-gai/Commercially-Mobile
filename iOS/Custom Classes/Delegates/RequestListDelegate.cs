using System;
using UIKit;

namespace Commercially.iOS
{
	public class RequestListDelegate : UITableViewDelegate
	{
		public RequestListDelegate(IntPtr handle) : base(handle)
		{
		}

		public RequestListDelegate(RequestListController controller)
		{
			//this.Controller = controller;
		}

		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
		}
	}
}
