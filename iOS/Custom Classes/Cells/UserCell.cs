// Created by Philip Gai

using System;
using Foundation;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// User cell.
	/// </summary>
	public partial class UserCell : UITableViewCell
	{
		UserTableRowManager Manager;

		public static readonly NSString Key = new NSString("UserCell");
		public static readonly UINib Nib;

		static UserCell()
		{
			Nib = UINib.FromName(Key, NSBundle.MainBundle);
		}

		protected UserCell(IntPtr handle) : base(handle) { }

		public User User {
			get {
				return Manager.User;
			}
			set {
				Manager = new UserTableRowManager(value);
				LastFirstNameLabel.Hidden = Manager.NameLabelIsHidden;
				LastFirstNameLabel.Text = Manager.NameText;
				EmailLabel.Text = Manager.EmailText;
			}
		}
	}
}
