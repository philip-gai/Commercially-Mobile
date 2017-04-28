using System;

using Foundation;
using UIKit;

namespace Commercially.iOS
{
	public partial class ClientCell : UITableViewCell
	{
		ClientTableRow SharedRow;

		public static readonly NSString Key = new NSString("ClientCell");
		public static readonly UINib Nib;

		static ClientCell()
		{
			Nib = UINib.FromName(Key, NSBundle.MainBundle);
		}

		protected ClientCell(IntPtr handle) : base(handle) { }

		public Client Client {
			get {
				return SharedRow.Client;
			}
			set {
				SharedRow = new ClientTableRow(value);
				IdLabel.Text = SharedRow.IdText;
				FriendlyNameLabel.Text = SharedRow.FriendlyNameText;
				FriendlyNameLabel.Hidden = SharedRow.FriendlyNameLabelIsHidden;
			}
		}
	}
}
