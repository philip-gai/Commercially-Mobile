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
			get {
				return _Request;
			}
			set {
				RoomLabel.Text = value.room ?? null;
				TimeLabel.Text = value.GetTime(Request.TimeType.Received) ?? "N/A";
				SetStatusLabel(value);
				UrgentIndicator.Hidden = !value.urgent;
				Message.Text = value.description;
				_Request = value;
			}
		}

		static RequestCell()
		{
			Nib = UINib.FromName(LocalConstants.ReuseIdentifiers.RequestCell, NSBundle.MainBundle);
		}

		protected RequestCell(IntPtr handle) : base(handle) { }

		public void SetStatusLabelIsHidden(bool isHidden)
		{
			StatusLabel.Hidden = isHidden;
		}

		void SetStatusLabel(Request request)
		{
			switch (request.GetStatus()) {
				case Status.New:
					StatusLabel.Text = Localizable.Labels.ToDo;
					break;
				case Status.Assigned:
					StatusLabel.Text = Localizable.Labels.InProgress;
					break;
				case Status.Completed:
					StatusLabel.Text = Localizable.Labels.Complete;
					break;
			}
		}
	}
}