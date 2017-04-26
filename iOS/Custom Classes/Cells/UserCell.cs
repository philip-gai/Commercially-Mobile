using System;

using Foundation;
using UIKit;

namespace Commercially.iOS
{
	public partial class UserCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("UserCell");
		public static readonly UINib Nib;

		static UserCell()
		{
			Nib = UINib.FromName(Key, NSBundle.MainBundle);
		}

		protected UserCell(IntPtr handle) : base(handle) { }

		User _User;
		public User User {
			get {
				return _User;
			}
			set {
				_User = value;
				LastFirstNameLabel.Hidden = string.IsNullOrWhiteSpace(value.firstname) || string.IsNullOrWhiteSpace(value.lastname);
				if (!LastFirstNameLabel.Hidden) {
					LastFirstNameLabel.Text = value.lastname + ", " + value.firstname;
				}
				EmailLabel.Text = value.email;
			}
		}
	}
}
