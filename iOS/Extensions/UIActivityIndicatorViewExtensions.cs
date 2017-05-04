// Created by Philip Gai

using CoreGraphics;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// UIActivity indicator view extensions.
	/// </summary>
	public static class UIActivityIndicatorViewExtensions
	{
		/// <summary>
		/// Sets the center of the indicator to the center of the view.
		/// </summary>
		/// <param name="indicator">Indicator.</param>
		/// <param name="centerInView">View to center in.</param>
		public static void SetCenter(this UIActivityIndicatorView indicator, UIView centerInView)
		{
			var Center = new CGPoint(centerInView.Center.X, centerInView.Bounds.GetMidY());
			if (indicator.Center != Center) {
				indicator.Center = Center;
			}
		}
	}
}
