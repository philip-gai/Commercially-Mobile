// Created by Philip Gai

using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// User details controller.
	/// </summary>
	public partial class UserDetailsController : UITableViewController
	{
		readonly UserDetailsManager Manager = new UserDetailsManager();

		public User User {
			set {
				Manager.User = value;
			}
		}

		public UserDetailsController(IntPtr handle) : base(handle) { }

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			if (Session.User.Type == UserRoleType.Admin) {
				GetRequests();
			} else {
				Session.User = UserApi.GetCurrentUser();
				Manager.User = Session.User;
			}
			InitializeView();
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			RemoveListeners();
		}

		void RemoveListeners()
		{
			ChangePasswordButton.TouchUpInside -= ChangePasswordButtonTouchUpInside;
			SaveButton.TouchUpInside -= SaveButtonTouchUpInside;
			NameField.EditingDidEnd -= FieldEditingDidEnd;
			UsernameField.EditingDidEnd -= FieldEditingDidEnd;
			EmailField.EditingDidEnd -= FieldEditingDidEnd;
			PhoneField.EditingDidEnd -= FieldEditingDidEnd;
			NewPasswordField.EditingDidEnd -= FieldEditingDidEnd;
			RepeatNewPasswordField.EditingDidEnd -= FieldEditingDidEnd;
		}

		void InitializeView()
		{
			if (Manager.User == null) return;

			InitializeFields();
			InitializeVisibility();

			if (Session.User.Type == UserRoleType.Admin) {
				TableView.Source = new UserRequestTableSource(this);
			}
			ChangePasswordButton.TouchUpInside += ChangePasswordButtonTouchUpInside;
			SaveButton.TouchUpInside += SaveButtonTouchUpInside;
		}

		void InitializeFields()
		{
			NameField.Text = Manager.NameText;
			UsernameField.Text = Manager.UsernameText;
			EmailField.Text = Manager.EmailText;
			PhoneField.Text = Manager.PhoneText;

			NameField.ResignOnReturn();
			UsernameField.ResignOnReturn();
			EmailField.ResignOnReturn();
			PhoneField.ResignOnReturn();
			OldPasswordField.ShouldReturn += (UITextField textField) => { NewPasswordField.BecomeFirstResponder(); return true; };
			NewPasswordField.ShouldReturn += (UITextField textField) => { RepeatNewPasswordField.BecomeFirstResponder(); return true; };
			RepeatNewPasswordField.ResignOnReturn();

			NameField.EditingDidEnd += FieldEditingDidEnd;
			UsernameField.EditingDidEnd += FieldEditingDidEnd;
			EmailField.EditingDidEnd += FieldEditingDidEnd;
			PhoneField.EditingDidEnd += FieldEditingDidEnd;
			NewPasswordField.EditingDidEnd += FieldEditingDidEnd;
			RepeatNewPasswordField.EditingDidEnd += FieldEditingDidEnd;
		}

		void InitializeVisibility()
		{
			OldPasswordField.Hidden = true;
			NewPasswordField.Hidden = true;
			RepeatNewPasswordField.Hidden = true;
			SaveButton.Hidden = true;
			ChangePasswordButton.Hidden = Manager.ChangePasswordButtonIsHidden;
		}

		void GetRequests()
		{
			Manager.GetRequests(delegate {
				InvokeOnMainThread(delegate {
					TableView.ReloadData();
				});
			}, (Exception e) => {
				InvokeOnMainThread(delegate {
					NavigationController.ShowPrompt(Localizable.PromptMessages.RequestsError);
				});
			});
		}

		void FieldEditingDidEnd(object sender, EventArgs e)
		{
			UIView.AnimateAsync(ButtonDetailsManager.AnimationDuration, delegate {
				SaveButton.Hidden = !Manager.FieldsAreChanged(NameField.Text, UsernameField.Text, EmailField.Text, PhoneField.Text,
																	NewPasswordField.Text, RepeatNewPasswordField.Text);
			});
		}

		void ChangePasswordButtonTouchUpInside(object sender, EventArgs e)
		{
			OldPasswordField.Hidden = !OldPasswordField.Hidden;
			NewPasswordField.Hidden = !NewPasswordField.Hidden;
			RepeatNewPasswordField.Hidden = !RepeatNewPasswordField.Hidden;
		}

		void SaveButtonTouchUpInside(object sender, EventArgs e)
		{
			try {
				Manager.OnSaveButtonPressHandler(NameField.Text, UsernameField.Text, EmailField.Text, PhoneField.Text,
												 OldPasswordField.Text, NewPasswordField.Text, RepeatNewPasswordField.Text);
			} catch (Exception) {
				if (UserDetailsManager.PasswordsAreChanged(NewPasswordField.Text, RepeatNewPasswordField.Text)) {
					NavigationController.ShowPrompt(Localizable.PromptMessages.InvalidPassword);
				} else {
					NavigationController.ShowPrompt(Localizable.PromptMessages.ChangesSaveError);
				}
				return;
			}

			SaveButton.Hidden = true;
			OldPasswordField.Hidden = true;
			NewPasswordField.Hidden = true;
			RepeatNewPasswordField.Hidden = true;
			if (Session.User.Type == UserRoleType.Admin) {
				NavigationController.PopViewController(true);
			} else {
				NavigationController.ShowPrompt(Localizable.PromptMessages.SaveSuccess);
				Session.User = UserApi.GetCurrentUser();
				User = Session.User;
			}
		}

		class UserRequestTableSource : UITableViewSource
		{
			readonly UserDetailsController Controller;
			UserDetailsManager Manager {
				get {
					return Controller.Manager;
				}
			}

			public UserRequestTableSource(UserDetailsController controller)
			{
				Controller = controller;
				Controller.TableView.RegisterNibForCellReuse(UINib.FromName(RequestCell.Key, null), RequestCell.Key);
			}

			public override nint NumberOfSections(UITableView tableView)
			{
				return 1;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return Manager.Requests == null ? 0 : Manager.Requests.Length;
			}

			public override nfloat GetHeightForHeader(UITableView tableView, nint section)
			{
				return (nfloat)UserDetailsManager.TableHeaderHeight;
			}

			public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
			{
				return (nfloat)UserDetailsManager.TableRowHeight;
			}

			public override UIView GetViewForHeader(UITableView tableView, nint section)
			{
				var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, (nfloat)UserDetailsManager.TableHeaderHeight));
				var Frame = new CGRect(10, 0, HeaderView.Frame.Width, (nfloat)UserDetailsManager.TableHeaderHeight);
				var Label = new UILabel(Frame);

				HeaderView.BackgroundColor = UserDetailsManager.TableHeaderColor.GetUIColor();
				Label.Text = UserDetailsManager.TableHeaderTitle;
				if (Manager.Requests != null) {
					Label.Text += " (" + Manager.Requests.Length + ")";
				}
				HeaderView.AddSubview(Label);
				return HeaderView;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(RequestCell.Key, indexPath) as RequestCell;
				cell.Request = Manager.Requests[indexPath.Row];
				cell.BackgroundColor = UserDetailsManager.TableHeaderColor.GetUIColor().ColorWithAlpha((nfloat)UserDetailsManager.TableRowAlphaDouble);
				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var nextController = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.RequestDetails) as RequestDetailsController;
				Controller.NavigationController.PushViewController(nextController, true);
				nextController.Request = Manager.Requests[indexPath.Row];
			}
		}
	}
}