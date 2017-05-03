using System;

using Foundation;
using UIKit;

namespace Commercially.iOS
{
	public partial class RequestCell : UITableViewCell
	{
		RequestTableRow SharedRow;

		public static readonly NSString Key = new NSString("RequestCell");
		public static readonly UINib Nib;

		static RequestCell()
		{
			Nib = UINib.FromName(Key, NSBundle.MainBundle);
		}

		protected RequestCell(IntPtr handle) : base(handle) { }

		public Request Request {
			get {
				return SharedRow.Request;
			}
			set {
				SharedRow = new RequestTableRow(value);
				LocationLabel.Text = SharedRow.LocationText;
				TimeLabel.Text = SharedRow.TimeText;
				StatusLabel.Text = SharedRow.StatusText;
				StatusLabel.Hidden = SharedRow.StatusLabelIsHidden;
				UrgentIndicator.Hidden = SharedRow.UrgentIndicatorIsHidden;
				Message.Text = SharedRow.DescriptionText;
			}
		}
	}
}