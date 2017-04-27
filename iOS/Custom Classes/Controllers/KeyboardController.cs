using System;
using Foundation;
using UIKit;

namespace Commercially.iOS
{
	public abstract class KeyboardController : UIViewController
	{
		NSObject keyboardShowToken, keyboardHideToken;

		public abstract UIScrollView ScrollView { get; }

		public KeyboardController(IntPtr handle) : base(handle) { }

		public override bool AutomaticallyAdjustsScrollViewInsets {
			get {
				return false;
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			View.HideKeyboardWhenTapped();
			ScrollView.Bounces = false;
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			keyboardShowToken = NotificationHelper.ObserveNotification(UIKeyboard.DidShowNotification, HandleKeyboardDidShow);
			keyboardHideToken = NotificationHelper.ObserveNotification(UIKeyboard.DidHideNotification, HandleKeyboardDidHide);
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			View.EndEditing(true);
			keyboardShowToken.Dispose();
			keyboardHideToken.Dispose();
		}

		void HandleKeyboardDidShow(NSNotification notification)
		{
			this.KeyboardDid(UIViewControllerExtensions.KeyboardActionType.Show, notification);
		}

		void HandleKeyboardDidHide(NSNotification notification)
		{
			this.KeyboardDid(UIViewControllerExtensions.KeyboardActionType.Hide, notification);
		}
	}
}
