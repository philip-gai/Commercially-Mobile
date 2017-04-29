using Foundation;
using System;
using UIKit;

namespace Commercially.iOS
{
	public partial class ClientDetailsController : KeyboardController
	{
		readonly ClientDetails SharedController = new ClientDetails();

		public ClientDetailsController(IntPtr handle) : base(handle) { }

		public Client Client {
			set {
				SharedController.Client = value;
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
			if (SharedController.Client == null) return;
			IdLabel.Text = SharedController.IdText;
			FriendlyNameField.Text = SharedController.FriendlyNameText;
			FriendlyNameField.Placeholder = SharedController.FriendlyNameFieldPlaceholder;

			SaveButton.Hidden = true;
			AuthorizeButton.Hidden = SharedController.AuthorizeButtonIsHidden;

			FriendlyNameField.ShouldReturn += (textField) => { textField.ResignFirstResponder(); return true; };
			FriendlyNameField.EditingDidEnd += FieldEditingDidEnd;

			SaveButton.TouchUpInside += SaveButtonTouchUpInside;
			AuthorizeButton.TouchUpInside += AuthorizeButtonTouchUpInside;
		}

		void FieldEditingDidEnd(object sender, EventArgs e)
		{
			SaveButton.Hidden = !SharedController.FriendlyNameChanged(FriendlyNameField.Text);
			UIView.AnimateAsync(ClientDetails.AnimationDuration, delegate {
				ButtonStack.Hidden = SaveButton.Hidden && AuthorizeButton.Hidden;
			});
		}

		void SaveButtonTouchUpInside(object sender, EventArgs e)
		{
			try {
				SharedController.SaveButtonPress(FriendlyNameField.Text);
			} catch (Exception) {
				NavigationController.ShowPrompt(Localizable.PromptMessages.ChangesSaveError);
				return;
			}
			SaveButton.Hidden = true;
			UIView.AnimateAsync(ClientDetails.AnimationDuration, delegate {
				ButtonStack.Hidden = AuthorizeButton.Hidden;
			});
			NavigationController.PopViewController(true);
		}

		void AuthorizeButtonTouchUpInside(object sender, EventArgs e)
		{
			try {
				SharedController.AuthorizeButtonPress();
			} catch (Exception) {
				NavigationController.ShowPrompt(Localizable.PromptMessages.ChangesSaveError);
				return;
			}
			AuthorizeButton.Hidden = true;
			UIView.AnimateAsync(ClientDetails.AnimationDuration, delegate {
				ButtonStack.Hidden = SaveButton.Hidden;
			});
			NavigationController.PopViewController(true);
		}
	}
}