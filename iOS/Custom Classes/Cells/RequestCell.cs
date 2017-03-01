using System;

using Foundation;
using UIKit;

namespace Commercially.iOS
{
	public partial class RequestCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString(LocalConstants.ReuseIdentifiers.RequestCell);
		public static readonly UINib Nib;

		static RequestCell()
		{
			Nib = UINib.FromName(LocalConstants.ReuseIdentifiers.RequestCell, NSBundle.MainBundle);
		}

		protected RequestCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void InitializeWithRequest(Request request) {
			RoomLabel.Text = request.Room;
			TimeLabel.Text = request.Received.ToShortTimeString();
			StatusLabel.Text = request.Status.ToString();
			UrgentIndicator.Hidden = !request.Urgent;
			Message.Text = request.Button.Message;
		}
	}
}