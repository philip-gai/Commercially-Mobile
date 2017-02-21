using System;
using UIKit;
using Commercially.iOS.Extensions;

namespace Commercially.iOS {
	public partial class RoundedUIButton : ActivityIndicatorButton {
		readonly static nfloat defaultCornerRadius = (nfloat)31;
		readonly static nfloat borderWidth = (nfloat)0.7;

		public RoundedUIButton(IntPtr handle) : base(handle) { }

		public override void AwakeFromNib() {
			base.AwakeFromNib();
			try {
				Layer.CornerRadius = this.GetConstraintConstant(NSLayoutAttribute.Height) / 2;
			} catch (NoConstraintMatchingException) {
				Layer.CornerRadius = defaultCornerRadius;
			}
			Layer.BorderWidth = borderWidth;
			if (CurrentTitleShadowColor != null) {
				Layer.BorderColor = CurrentTitleShadowColor.CGColor;
			} else {
				Layer.BorderColor = UIColor.White.CGColor;
			}
		}
	}
}