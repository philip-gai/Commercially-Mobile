using System;

using Foundation;
using UIKit;

namespace Commercially.iOS
{
	public partial class ButtonCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("ButtonCell");
		public static readonly UINib Nib;

		static ButtonCell()
		{
			Nib = UINib.FromName(Key, NSBundle.MainBundle);
		}

		protected ButtonCell(IntPtr handle) : base(handle) { }

		FlicButton _Button;
		public FlicButton Button {
			get {
				return _Button;
			}
			set {
				_Button = value;
				var tmpClient = Client.FindClient(Button.clientId, Session.Clients);
				ClientLabel.Text = tmpClient != null && tmpClient.friendlyName != null ? tmpClient.friendlyName : value.clientId;
				LocationLabel.Text = value.room ?? null;
				DescriptionLabel.Text = value.description ?? null;
			}
		}
	}
}
