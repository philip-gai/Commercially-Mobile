using System;
using UIKit;
using Commercially.iOS.Extensions;

namespace Commercially.iOS {
	public abstract class UnderlineViewController : UIViewController {
		public abstract UIView[] UnderlineViews { get; }
		public abstract UIView ViewForUnderlines { get; }
		public abstract bool ShowNavigationBar { get; }

		static int IsDoneLayoutSubviews = 1;
		int LayoutCount = 0;

		public UnderlineViewController(IntPtr handle) : base(handle) { }

		public override void ViewWillAppear(bool animated) {
			base.ViewWillAppear(animated);
			if (ShowNavigationBar) {
				NavigationController.SetNavigationBarHidden(false, false);
			}
		}

		public override void ViewWillDisappear(bool animated) {
			base.ViewWillDisappear(animated);
			if (ShowNavigationBar) {
				NavigationController.SetNavigationBarHidden(true, false);
			}
		}

		public override void ViewDidLayoutSubviews() {
			base.ViewDidLayoutSubviews();
			if (LayoutCount == IsDoneLayoutSubviews) {
				SetLines();
			}
			LayoutCount++;
		}

		void SetLines() {
			if (UnderlineViews == null) return;
			foreach (UIView view in UnderlineViews) {
				UIView lineView = ViewForUnderlines.AddUnderlineView(view, LocalConstants.LineColor);
				if (view is UnderlineField) {
					var field = view as UnderlineField;
					if (field.GetUnderlineView() != null && field.GetUnderlineView().IsDescendantOfView(View)) {
						field.GetUnderlineView().RemoveFromSuperview();
					}
					field.SetUnderlineView(lineView);
				} else if (view is UnderlineButton) {
					var button = view as UnderlineButton;
					if (button.GetUnderlineView() != null && button.GetUnderlineView().IsDescendantOfView(View)) {
						button.GetUnderlineView().RemoveFromSuperview();
					}
					button.SetUnderlineView(lineView);
				}
			}
		}

		public void SetLineColors(object sender, EventArgs events) {
			if (UnderlineViews == null) return;
			foreach (UIView view in UnderlineViews) {
				if (view is UnderlineButton) {
					var underlineButton = view as UnderlineButton;
					underlineButton.SetLineColor(underlineButton.IsValid());
				}
			}
		}
	}
}
