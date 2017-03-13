using System;

using Foundation;
using UIKit;

namespace Commercially.iOS
{
	public partial class RequestCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString(LocalConstants.ReuseIdentifiers.RequestCell);
		public static readonly UINib Nib;

		Request _Request;
		public Request Request {
			set {
				_Request = value;
				RoomLabel.Text = value.room;
				TimeLabel.Text = value.time_received.ToShortTimeString();
				SetStatusLabel(value);
				UrgentIndicator.Hidden = !value.urgent;
				Message.Text = value.description;
			}
		}

		static RequestCell()
		{
			Nib = UINib.FromName(LocalConstants.ReuseIdentifiers.RequestCell, NSBundle.MainBundle);
		}

		protected RequestCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void SetStatusLabelIsHidden(bool isHidden)
		{
			if (isHidden) {
				StatusLabel.Text = "";
			} else {
				SetStatusLabel(_Request);
			}
		}

		void SetStatusLabel(Request request)
		{
			switch (request.Status) {
				case Status.ToDo:
					StatusLabel.Text = Localizable.Labels.ToDo;
					break;
				case Status.InProgress:
					StatusLabel.Text = Localizable.Labels.InProgress;
					break;
				case Status.Complete:
					StatusLabel.Text = Localizable.Labels.Complete;
					break;
			}
		}
	}
}