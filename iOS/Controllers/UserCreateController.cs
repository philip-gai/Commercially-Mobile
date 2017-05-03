using Foundation;
using System;
using UIKit;

namespace Commercially.iOS
{
	public partial class UserCreateController : KeyboardController
	{
		public UserCreateController(IntPtr handle) : base(handle)
		{
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
			InitializeFields();
			UserRoleTypePicker.Model = new UserRolePickerModel();
			CreateButton.TouchUpInside += CreateButtonTouchUpInside;
		}

		void InitializeFields()
		{
			NameField.ShouldReturn += (UITextField textField) => { UsernameField.BecomeFirstResponder(); return true; };
			UsernameField.ResignOnReturn();
			EmailField.ShouldReturn += (UITextField textField) => { PhoneField.BecomeFirstResponder(); return true; };
			PhoneField.ShouldReturn += (UITextField textField) => { PasswordField.BecomeFirstResponder(); return true; };
			PasswordField.ShouldReturn += (UITextField textField) => { VerifyPasswordField.BecomeFirstResponder(); return true; };
			VerifyPasswordField.ResignOnReturn();
		}

		void CreateButtonTouchUpInside(object sender, EventArgs e)
		{
			try {
				UserCreate.CreateButtonPress(NameField.Text, UsernameField.Text,
											 UserRoleTypePicker.Model.GetTitle(UserRoleTypePicker, UserRoleTypePicker.SelectedRowInComponent(0), 0),
											 EmailField.Text, PhoneField.Text, PasswordField.Text, VerifyPasswordField.Text);
			} catch (Exception) {
				NavigationController.ShowPrompt(Localizable.PromptMessages.CannotCreateUser);
				return;
			}

			NavigationController.PopViewController(true);
			NavigationController.ShowPrompt(Localizable.PromptMessages.UserCreateSuccess);
		}
	}
}