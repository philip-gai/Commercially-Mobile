// Created by Philip Gai

using UIKit;
namespace Commercially.iOS
{
	/// <summary>
	/// Color extensions.
	/// </summary>
	public static class ColorExtensions
	{
		/// <summary>
		/// Gets the UI Color of the Color.
		/// </summary>
		/// <returns>The UIColor.</returns>
		/// <param name="color">Color.</param>
		public static UIColor GetUIColor(this Color color)
		{
			return UIColor.FromRGB(color.R, color.G, color.B);
		}
	}
}
