using System;

using Foundation;
using UIKit;

namespace Commercially.iOS
{
	public partial class UserCell : UITableViewCell
	{
		UserTableRow SharedRow;

		public static readonly NSString Key = new NSString("UserCell");
		public static readonly UINib Nib;

		static UserCell()
		{
			Nib = UINib.FromName(Key, NSBundle.MainBundle);
		}

		protected UserCell(IntPtr handle) : base(handle) { }

		public User User {
			get {
				return SharedRow.User;
			}
			set {
				SharedRow = new UserTableRow(value);
				LastFirstNameLabel.Hidden = SharedRow.LastFirstNameLabelIsHidden;
				LastFirstNameLabel.Text = SharedRow.LastFirstNameText;
				EmailLabel.Text = SharedRow.EmailText;
			}
		}
	}
}
