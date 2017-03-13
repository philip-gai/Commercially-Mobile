using Foundation;
using System;
using UIKit;

namespace Commercially.iOS {
	public partial class PromptController : UIViewController {
		string TmpText = "";

		public PromptController(IntPtr handle) : base(handle) { }

		public override void ViewDidLoad() {
			base.ViewDidLoad();
			PromptLabel.Text = TmpText;
			DismissButton.TouchUpInside += DismissPrompt;
			BackgroundButton.TouchUpInside += DismissPrompt;
		}

		public override void ViewDidUnload() {
			base.ViewDidUnload();
			DismissButton.TouchUpInside -= DismissPrompt;
			BackgroundButton.TouchUpInside -= DismissPrompt;
		}

		public void SetPrompt(string prompt) {
			TmpText = prompt;
		}

		public void DismissPrompt(object sender, EventArgs events) {
			DismissModalViewController(true);
			NSNotificationCenter.DefaultCenter.PostNotificationName(LocalConstants.Notifications.HidePrompt, null);
		}
	}
}