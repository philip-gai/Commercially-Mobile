using System;
using UIKit;
using CoreGraphics;

namespace Commercially.iOS
{
	public static class UIViewExtensions
	{
		public static UIView AddUnderlineView(this UIView view, UIView subview, UIColor color)
		{
			CGRect Bounds = subview.ConvertRectToView(subview.Bounds, view);
			nfloat LineWidth = subview.Frame.Width;
			if (subview is UITextField && subview.Superview is UIStackView) {
				var stackView = subview.Superview as UIStackView;
				if (stackView.Axis == UILayoutConstraintAxis.Horizontal) {
					LineWidth = stackView.Frame.Width;
				}
			}
			var lineFrame = new CGRect(Bounds.Left, Bounds.Bottom + LocalConstants.LineHeight, LineWidth, LocalConstants.LineHeight);
			var lineView = new UIView(lineFrame);
			lineView.BackgroundColor = color;
			view.AddSubview(lineView);
			return lineView;
		}

		public static nfloat GetConstraintConstant(this UIView view, NSLayoutAttribute attribute)
		{
			foreach (NSLayoutConstraint constraint in view.Constraints) {
				if (constraint.FirstAttribute == attribute || constraint.SecondAttribute == attribute) {
					return constraint.Constant;
				}
			}
			throw new NoConstraintMatchingException(Localizable.ExceptionMessages.NoConstraint);
		}

		public static UIView GetFirstResponder(this UIView view)
		{
			foreach (UIView subview in view.Subviews) {
				if (view.IsFirstResponder) {
					return view;
				}
			}
			return null;
		}

		public static UITextField GetActiveField(this UIView view)
		{
			var FirstResponder = view.GetFirstResponder();
			if (FirstResponder is UITextField) {
				return (FirstResponder as UITextField);
			}
			return null;
		}

		public static void SetBackgroundFromImageName(this UIView view, string imageName)
		{
			var image = UIImage.FromFile(imageName);
			var backgroundView = new UIImageView(image);
			view.SetAnchorsToEdges(backgroundView);
			view.SendSubviewToBack(backgroundView);
		}

		public static void SetAnchorsToEdges(this UIView view, UIView subview)
		{
			UILayoutGuide margins = view.LayoutMarginsGuide;
			subview.LeadingAnchor.ConstraintEqualTo(margins.LeadingAnchor).Active = true;
			subview.TrailingAnchor.ConstraintEqualTo(margins.TrailingAnchor).Active = true;
			subview.BottomAnchor.ConstraintEqualTo(margins.BottomAnchor).Active = true;
			subview.TopAnchor.ConstraintEqualTo(margins.TopAnchor).Active = true;
		}

		public static void HideKeyboardWhenTapped(this UIView view)
		{
			var tap = new UITapGestureRecognizer((obj) => { view.EndEditing(true); });
			view.AddGestureRecognizer(tap);
		}

		public static void RemoveHideKeyboardWhenTapped(this UIView view)
		{
			foreach (UIGestureRecognizer recognizer in view.GestureRecognizers) {
				if (recognizer is UITapGestureRecognizer) {
					view.RemoveGestureRecognizer(recognizer);
					break;
				}
			}
		}

		public enum ScaleType { Width, Height };
		public static void ScaleView(this UIView view, ScaleType type, double newValue)
		{
			nfloat nfNewValue = (nfloat)newValue;
			nfloat width = nfNewValue;
			nfloat height = nfNewValue;
			switch (type) {
				case ScaleType.Width:
					height = (nfNewValue / view.Bounds.Width) * view.Bounds.Height;
					break;
				case ScaleType.Height:
					width = (nfNewValue / view.Bounds.Height) * view.Bounds.Width;
					break;
			}
			view.Frame = new CGRect(view.Frame.X, view.Frame.Y, width, height);
		}
	}
}
