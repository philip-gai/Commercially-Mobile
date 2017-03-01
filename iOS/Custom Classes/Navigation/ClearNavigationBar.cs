using System;
using Foundation;
using UIKit;

namespace Commercially.iOS {
	public partial class ClearNavigationBar : UINavigationBar {
		public ClearNavigationBar(IntPtr handle) : base(handle) { }

		public override void AwakeFromNib() {
			base.AwakeFromNib();
			SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
			//SetTitleAttributes();
		}

		// Gets rid of black line at bottom of bar
		public override UIImage ShadowImage {
			get {
				return new UIImage();
			}
		}

		void SetTitleAttributes() {
			Appearance.SetTitleTextAttributes(new UITextAttributes() {
				TextColor = UIColor.White,
				TextShadowColor = UIColor.Clear,
				Font = TitleTextAttributes.Font.WithSize(16)
			});
		}
	}
}