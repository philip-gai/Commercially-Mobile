using UIKit;
using CoreGraphics;

namespace Commercially.iOS
{
	public static class UIActivityIndicatorViewExtensions
	{
		public static void SetCenter(this UIActivityIndicatorView indicator, UIView centerInView)
		{
			var Center = new CGPoint(centerInView.Center.X, centerInView.Bounds.GetMidY());
			if (indicator.Center != Center) {
				indicator.Center = Center;
			}
		}
	}
}
