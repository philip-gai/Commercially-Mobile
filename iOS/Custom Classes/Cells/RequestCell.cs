// Created by Philip Gai

using System;
using Foundation;
using UIKit;

namespace Commercially.iOS
{
	public partial class RequestCell : UITableViewCell
	{
		RequestTableRowManager Manager;

		public static readonly NSString Key = new NSString("RequestCell");
		public static readonly UINib Nib;

		static RequestCell()
		{
			Nib = UINib.FromName(Key, NSBundle.MainBundle);
		}

		protected RequestCell(IntPtr handle) : base(handle) { }

		public Request Request {
			get {
				return Manager.Request;
			}
			set {
				Manager = new RequestTableRowManager(value);
				LocationLabel.Text = Manager.LocationText;
				TimeLabel.Text = Manager.ReceivedTimeText;
				StatusLabel.Text = Manager.StatusText;
				StatusLabel.Hidden = Manager.StatusLabelIsHidden;
				UrgentIndicator.Hidden = Manager.UrgentIndicatorIsHidden;
				Message.Text = Manager.DescriptionText;
			}
		}
	}
}