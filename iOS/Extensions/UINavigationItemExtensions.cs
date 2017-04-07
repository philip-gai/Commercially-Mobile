using UIKit;
using CoreGraphics;

namespace Commercially.iOS.Extensions {
	public static class UINavigationItemExtensions {
		public static void SetTitleImage(this UINavigationItem item, string imageName) {
			var image = UIImage.FromBundle(imageName);
			var imageView = new UIImageView(image);
			item.TitleView = imageView;
		}
	}
}
