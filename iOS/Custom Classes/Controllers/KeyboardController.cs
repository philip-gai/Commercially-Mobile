// Created by Philip Gai

using System;
using Foundation;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// Keyboard controller.
	/// </summary>
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
			keyboardShowToken = UIKeyboard.Notifications.ObserveDidShow(HandleKeyboardDidShow);
			keyboardHideToken = UIKeyboard.Notifications.ObserveDidHide(HandleKeyboardDidHide);
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			View.EndEditing(true);
			keyboardShowToken.Dispose();
			keyboardHideToken.Dispose();
		}

		void HandleKeyboardDidShow(object sender, UIKeyboardEventArgs e)
		{
			this.KeyboardDid(UIViewControllerExtensions.KeyboardActionType.Show, e.FrameEnd);
		}

		void HandleKeyboardDidHide(object sender, UIKeyboardEventArgs e)
		{
			this.KeyboardDid(UIViewControllerExtensions.KeyboardActionType.Hide, e.FrameEnd);
		}
	}
}
