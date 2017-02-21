using UIKit;
using CoreGraphics;

namespace Commercially.iOS.Extensions {
	public static class UINavigationItemExtensions {
		public static void SetTitleImage(this UINavigationItem item, string imageName, UINavigationBar bar) {
			var height = bar.Bounds.Height / 2;
			var image = UIImage.FromBundle(imageName);
			image = image.Scale(new CGSize((height/image.Size.Height)*image.Size.Width, height));
			var imageView = new UIImageView(image);
			item.TitleView = imageView;
		}
	}
}
