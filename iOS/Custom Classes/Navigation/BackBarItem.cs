using Foundation;
using System;
using UIKit;

namespace Commercially.iOS {
	public partial class BackBarItem : UIBarButtonItem {
		public BackBarItem(IntPtr handle) : base(handle) { }

		public override void AwakeFromNib() {
			base.AwakeFromNib();
			Target = this;
			Action = new ObjCRuntime.Selector(LocalConstants.Selectors.PopAction);
		}

		[Export(LocalConstants.Selectors.PopAction)]
		void popAction() {
			NotificationHelper.PopViewController();
		}
	}
}