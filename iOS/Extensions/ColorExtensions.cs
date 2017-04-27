using UIKit;
namespace Commercially.iOS
{
	public static class ColorExtensions
	{
		public static UIColor GetUIColor(this Color color)
		{
			return UIColor.FromRGB(color.R, color.G, color.B);
		}
	}
}
