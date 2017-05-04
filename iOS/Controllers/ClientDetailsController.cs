// Created by Philip Gai

using System;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// Client details controller.
	/// </summary>
	public partial class ClientDetailsController : KeyboardController
	{
		readonly ClientDetailsManager Manager = new ClientDetailsManager();

		public ClientDetailsController(IntPtr handle) : base(handle) { }

		public Client Client {
			set {
				Manager.Client = value;
			}
		}

		public override UIScrollView ScrollView {
			get {
				return KeyboardScrollView;
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			InitializeView();
		}

		void InitializeView()
		{
			if (Manager.Client == null) return;
			IdLabel.Text = Manager.IdText;
			FriendlyNameField.Text = Manager.FriendlyNameText;
			FriendlyNameField.Placeholder = Manager.FriendlyNameFieldPlaceholder;

			SaveButton.Hidden = true;
			AuthorizeButton.Hidden = Manager.AuthorizeButtonIsHidden;

			FriendlyNameField.ResignOnReturn();
			FriendlyNameField.EditingDidEnd += FieldEditingDidEnd;

			SaveButton.TouchUpInside += SaveButtonTouchUpInside;
			AuthorizeButton.TouchUpInside += AuthorizeButtonTouchUpInside;
		}

		void FieldEditingDidEnd(object sender, EventArgs e)
		{
			SaveButton.Hidden = !Manager.FriendlyNameIsChanged(FriendlyNameField.Text);
			UIView.AnimateAsync(ClientDetailsManager.AnimationDuration, delegate {
				ButtonStack.Hidden = SaveButton.Hidden && AuthorizeButton.Hidden;
			});
		}

		void SaveButtonTouchUpInside(object sender, EventArgs e)
		{
			try {
				Manager.OnSaveButtonPressHandler(FriendlyNameField.Text);
			} catch (Exception) {
				NavigationController.ShowPrompt(Localizable.PromptMessages.ChangesSaveError);
				return;
			}
			SaveButton.Hidden = true;
			ButtonStack.Hidden = AuthorizeButton.Hidden;
			NavigationController.PopViewController(true);
		}

		void AuthorizeButtonTouchUpInside(object sender, EventArgs e)
		{
			try {
				Manager.OnAuthorizeButtonPressHandler();
			} catch (Exception) {
				NavigationController.ShowPrompt(Localizable.PromptMessages.AuthorizeError);
				return;
			}
			AuthorizeButton.Hidden = true;
			UIView.AnimateAsync(ClientDetailsManager.AnimationDuration, delegate {
				ButtonStack.Hidden = SaveButton.Hidden;
			});
			NavigationController.PopViewController(true);
		}
	}
}