using System;

using Foundation;
using UIKit;

namespace Commercially.iOS
{
	public partial class RequestCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("RequestCell");
		public static readonly UINib Nib;

		static RequestCell()
		{
			Nib = UINib.FromName("RequestCell", NSBundle.MainBundle);
		}

		protected RequestCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}
	}
}
