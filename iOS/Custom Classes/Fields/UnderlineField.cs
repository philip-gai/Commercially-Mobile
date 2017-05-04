// Created by Philip Gai

using System;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// Underline field.
	/// </summary>
	public partial class UnderlineField : AbstractField
	{
		UIView UnderlineView;
		UIColor UnderlineColor;

		public UnderlineField(IntPtr handle) : base(handle) { }

		public void SetUnderlineView(UIView view, UIColor UnderlineColor)
		{
			UnderlineView = view;
			this.UnderlineColor = UnderlineColor;
		}

		public UIView GetUnderlineView()
		{
			return UnderlineView;
		}

		public void ClearUnderlineView()
		{
			UnderlineView = null;
		}

		public void SetLineColor(bool valid)
		{
			if (UnderlineView == null) return;
			if (valid) {
				UnderlineView.BackgroundColor = UnderlineColor;
			} else {
				UnderlineView.BackgroundColor = LocalConstants.LineIncompleteColor;
			}
		}

		public override void SetNextField(AbstractField nextResponder)
		{
			if (!(nextResponder is UnderlineField)) return;
			NextField = nextResponder;
			Delegate = new UnderlineFieldDelegate(this, (nextResponder as UnderlineField));
		}

		public override UITextFieldDelegate FieldDelegate {
			get {
				return new UnderlineFieldDelegate(this);
			}
		}
	}
}
