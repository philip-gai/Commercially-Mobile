using System;

using Foundation;
using UIKit;

namespace Commercially.iOS
{
	public partial class ButtonCell : UITableViewCell
	{
		ButtonTableRow SharedRow;

		public static readonly NSString Key = new NSString("ButtonCell");
		public static readonly UINib Nib;

		static ButtonCell()
		{
			Nib = UINib.FromName(Key, NSBundle.MainBundle);
		}

		protected ButtonCell(IntPtr handle) : base(handle) { }

		public FlicButton Button {
			get {
				return SharedRow.Button;
			}
			set {
				SharedRow = new ButtonTableRow(value);
				ButtonLabel.Text = SharedRow.ButtonText;
				ClientLabel.Text = SharedRow.ClientText;
				ClientLabel.Hidden = SharedRow.ClientLabelIsHidden;
				DescriptionLabel.Text = SharedRow.DescriptionText;
				LocationLabel.Text = SharedRow.LocationText;
			}
		}
	}
}
