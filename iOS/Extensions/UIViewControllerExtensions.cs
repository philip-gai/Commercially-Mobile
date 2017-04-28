using UIKit;
using System;
using Foundation;
using CoreGraphics;

namespace Commercially.iOS
{
	public static class UIViewControllerExtensions
	{
		public enum KeyboardActionType { Show, Hide };

		public static void KeyboardDid(this UIViewController controller, KeyboardActionType Type, CGRect KeyboardFrame)
		{
			UIView view = controller.View;
			view.SetNeedsLayout();
			view.LayoutIfNeeded();

			UIScrollView scrollView = null;
			var scrollViewFrame = new CGRect();
			if (controller is FieldListController) {
				var inputView = controller as FieldListController;
				scrollView = inputView.ScrollView;
				scrollViewFrame = scrollView.ConvertRectToView(scrollView.Bounds, view);
			}
			if (controller is KeyboardController) {
				var inputView = controller as KeyboardController;
				scrollView = inputView.ScrollView;
				scrollViewFrame = scrollView.ConvertRectToView(scrollView.Bounds, view);
			}

			nfloat nextY = view.Frame.Y;
			switch (Type) {
				case KeyboardActionType.Show:
					if (scrollView != null && scrollView.Frame.Height != scrollView.Superview.Frame.Height - KeyboardFrame.Height) {
						scrollView.Frame = new CGRect(scrollViewFrame.X, scrollViewFrame.Y, scrollViewFrame.Width, scrollView.Superview.Frame.Height - KeyboardFrame.Height);
					}
					break;
				case KeyboardActionType.Hide:
					if (scrollView != null) {
						scrollView.Frame = new CGRect(scrollViewFrame.X, scrollViewFrame.Y, scrollViewFrame.Width, scrollView.Superview.Frame.Height);
					}
					break;
			}

			var field = controller.View.GetActiveField();
			if (field == null) return;
			scrollView.ScrollRectToVisible(field.Bounds, true);
		}
	}
}
