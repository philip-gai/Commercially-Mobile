// Created by Philip Gai

using System;
using Foundation;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// Button cell.
	/// </summary>
	public partial class ButtonCell : UITableViewCell
	{
		ButtonTableRowManager Manager;

		public static readonly NSString Key = new NSString("ButtonCell");
		public static readonly UINib Nib;

		static ButtonCell()
		{
			Nib = UINib.FromName(Key, NSBundle.MainBundle);
		}

		protected ButtonCell(IntPtr handle) : base(handle) { }

		public FlicButton Button {
			get {
				return Manager.Button;
			}
			set {
				Manager = new ButtonTableRowManager(value);
				ButtonLabel.Text = Manager.ButtonText;
				ClientLabel.Text = Manager.ClientText;
				ClientLabel.Hidden = Manager.ClientLabelIsHidden;
				DescriptionLabel.Text = Manager.DescriptionText;
				LocationLabel.Text = Manager.LocationText;
			}
		}
	}
}
