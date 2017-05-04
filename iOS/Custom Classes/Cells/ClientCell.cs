// Created by Philip Gai

using System;
using Foundation;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// Client cell.
	/// </summary>
	public partial class ClientCell : UITableViewCell
	{
		ClientTableRowManager Manager;

		public static readonly NSString Key = new NSString("ClientCell");
		public static readonly UINib Nib;

		static ClientCell()
		{
			Nib = UINib.FromName(Key, NSBundle.MainBundle);
		}

		protected ClientCell(IntPtr handle) : base(handle) { }

		public Client Client {
			get {
				return Manager.Client;
			}
			set {
				Manager = new ClientTableRowManager(value);
				IdLabel.Text = Manager.IdText;
				FriendlyNameLabel.Text = Manager.FriendlyNameText;
				FriendlyNameLabel.Hidden = Manager.FriendlyNameLabelIsHidden;
			}
		}
	}
}
