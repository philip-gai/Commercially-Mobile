// Created by Philip Gai

using System;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// Activity indicator button.
	/// </summary>
	public partial class ActivityIndicatorButton : UIButton
	{
		string TitleText = "";
		UIActivityIndicatorView LoadingIndicator = new UIActivityIndicatorView();

		public ActivityIndicatorButton(IntPtr handle) : base(handle) { }

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();
			SetLoadingIndicatorSubview();
		}

		public void ActivityStarted()
		{
			Enabled = false;
			HideTitle();
			LoadingIndicator.SetCenter(this); ;
			LoadingIndicator.StartAnimating();
		}

		public void ActivityEnded()
		{
			LoadingIndicator.StopAnimating();
			ShowTitle();
			Enabled = true;
		}

		void HideTitle()
		{
			TitleText = TitleLabel.Text;
			SetTitle("", UIControlState.Normal);
		}

		void ShowTitle()
		{
			SetTitle(TitleText, UIControlState.Normal);
		}

		void SetLoadingIndicatorSubview()
		{
			LoadingIndicator.SetCenter(this);
			LoadingIndicator.HidesWhenStopped = true;
			LoadingIndicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray;
			AddSubview(LoadingIndicator); ;
		}
	}
}