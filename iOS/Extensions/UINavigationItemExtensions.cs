// Created by Philip Gai

using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// UINavigation item extensions.
	/// </summary>
	public static class UINavigationItemExtensions
	{
		/// <summary>
		/// Sets the title image of the navigation item.
		/// </summary>
		/// <param name="item">Item.</param>
		/// <param name="imageName">Image name.</param>
		public static void SetTitleImage(this UINavigationItem item, string imageName)
		{
			var image = UIImage.FromBundle(imageName);
			var imageView = new UIImageView(image);
			item.TitleView = imageView;
		}
	}
}
