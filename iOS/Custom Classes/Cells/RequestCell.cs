using System;

using Foundation;
using UIKit;

namespace Commercially.iOS
{
	public partial class RequestCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("RequestCell");
		public static readonly UINib Nib;

		static RequestCell()
		{
			Nib = UINib.FromName(Key, NSBundle.MainBundle);
		}

		protected RequestCell(IntPtr handle) : base(handle) { }

		Request _Request;
		public Request Request {
			get {
				return _Request;
			}
			set {
				_Request = value;
				LocationLabel.Text = value.room;
				TimeLabel.Text = value.GetTime(Request.TimeType.Received)?.ToShortTimeString();
				StatusLabel.Text = _Request.Type.ToString();
				StatusLabel.Hidden = true;
				UrgentIndicator.Hidden = !value.urgent;
				Message.Text = value.description;
			}
		}
	}
}