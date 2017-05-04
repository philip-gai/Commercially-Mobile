// Created by Philip Gai

using System;
using CoreGraphics;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// UIView extensions.
	/// </summary>
	public static class UIViewExtensions
	{
		/// <summary>
		/// Adds the underline view.
		/// </summary>
		/// <returns>The underline view.</returns>
		/// <param name="view">View.</param>
		/// <param name="subview">Subview.</param>
		/// <param name="color">Color.</param>
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

		/// <summary>
		/// Gets the constraint constant.
		/// </summary>
		/// <returns>The constraint constant.</returns>
		/// <param name="view">View.</param>
		/// <param name="attribute">Attribute.</param>
		public static nfloat GetConstraintConstant(this UIView view, NSLayoutAttribute attribute)
		{
			foreach (NSLayoutConstraint constraint in view.Constraints) {
				if (constraint.FirstAttribute == attribute || constraint.SecondAttribute == attribute) {
					return constraint.Constant;
				}
			}
			throw new NoConstraintMatchingException(Localizable.ExceptionMessages.NoConstraint);
		}

		/// <summary>
		/// Gets the first responder.
		/// </summary>
		/// <returns>The first responder.</returns>
		/// <param name="view">View.</param>
		public static UIView GetFirstResponder(this UIView view)
		{
			foreach (UIView subview in view.Subviews) {
				if (view.IsFirstResponder) {
					return view;
				}
			}
			return null;
		}

		/// <summary>
		/// Gets the active field.
		/// </summary>
		/// <returns>The active field.</returns>
		/// <param name="view">View.</param>
		public static UITextField GetActiveField(this UIView view)
		{
			var FirstResponder = view.GetFirstResponder();
			if (FirstResponder is UITextField) {
				return (FirstResponder as UITextField);
			}
			return null;
		}

		/// <summary>
		/// Hides the keyboard when tapped.
		/// </summary>
		/// <param name="view">View.</param>
		public static void HideKeyboardWhenTapped(this UIView view)
		{
			var tap = new UITapGestureRecognizer((obj) => { view.EndEditing(true); });
			view.AddGestureRecognizer(tap);
		}

		/// <summary>
		/// Removes the hide keyboard when tapped.
		/// </summary>
		/// <param name="view">View.</param>
		public static void RemoveHideKeyboardWhenTapped(this UIView view)
		{
			foreach (UIGestureRecognizer recognizer in view.GestureRecognizers) {
				if (recognizer is UITapGestureRecognizer) {
					view.RemoveGestureRecognizer(recognizer);
					break;
				}
			}
		}
	}
}
