using UIKit;
namespace Commercially.iOS.Extensions {
	public static class UITableViewControllerExtensions {
		public static void SetBackgroundFromImageName(this UITableView tableView, string imageName) {
			var image = UIImage.FromBundle(imageName);
			if (image == null) return; // Should throw some exception for receiver to handle
			var backgroundView = new UIImageView(image);
			backgroundView.ContentMode = UIViewContentMode.ScaleAspectFill;
			tableView.BackgroundView = backgroundView;
		}
	}
}
