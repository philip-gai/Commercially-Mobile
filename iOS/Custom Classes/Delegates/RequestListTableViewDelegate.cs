using System;
using UIKit;

namespace Commercially.iOS
{
	public class RequestListTableViewDelegate : UITableViewDelegate
	{
		public RequestListTableViewDelegate(IntPtr handle) : base (handle)
        {
		}

		public override void WillDisplay(UITableView tableView, UITableViewCell cell, Foundation.NSIndexPath indexPath)
		{
			base.WillDisplay(tableView, cell, indexPath);
		}

		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			base.RowSelected(tableView, indexPath);
		}
	}
}
