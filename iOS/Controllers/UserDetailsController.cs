using Foundation;

using System;
using UIKit;
using CoreGraphics;

namespace Commercially.iOS
{
	public partial class UserDetailsController : UITableViewController
	{
		readonly UserDetails SharedController = new UserDetails();

		public User User {
			set {
				SharedController.User = value;
			}
		}

		public UserDetailsController(IntPtr handle) : base(handle) { }

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			GetRequests();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			InitializeView();
		}

		void InitializeView()
		{
			if (SharedController.User == null) return;
			if (Session.User.Type == UserRoleType.Admin) {
				TableView.Source = new UserRequestTableSource(this);
			}
			NameField.Text = SharedController.NameText;
			EmailField.Text = SharedController.EmailText;
			PhoneField.Text = SharedController.PhoneText;

			NameField.ResignOnReturn();
			EmailField.ResignOnReturn();
			PhoneField.ResignOnReturn();
			OldPasswordField.ShouldReturn += (UITextField textField) => { NewPasswordField.BecomeFirstResponder(); return true; };
			NewPasswordField.ShouldReturn += (UITextField textField) => { RepeatNewPasswordField.BecomeFirstResponder(); return true; };
			RepeatNewPasswordField.ResignOnReturn();

			PhoneField.Hidden = SharedController.PhoneFieldIsHidden;
			OldPasswordField.Hidden = true;
			NewPasswordField.Hidden = true;
			RepeatNewPasswordField.Hidden = true;
			SaveButton.Hidden = true;
			ChangePasswordButton.TouchUpInside += ChangePasswordButtonTouchUpInside;
			SaveButton.TouchUpInside += SaveButtonTouchUpInside;

			if (!SharedController.IsEditable) {
				NameField.DisguiseAsTextView();
				EmailField.DisguiseAsTextView();
				PhoneField.DisguiseAsTextView();
				ChangePasswordButton.Hidden = true;
			} else {
				NameField.EditingDidEnd += FieldEditingDidEnd;
				EmailField.EditingDidEnd += FieldEditingDidEnd;
				PhoneField.EditingDidEnd += FieldEditingDidEnd;
				NewPasswordField.EditingDidEnd += FieldEditingDidEnd;
				RepeatNewPasswordField.EditingDidEnd += FieldEditingDidEnd;
			}
		}

		void GetRequests()
		{
			SharedController.GetRequests(delegate {
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
			UIView.AnimateAsync(ButtonDetails.AnimationDuration, delegate {
				SaveButton.Hidden = !SharedController.FieldsChanged(NameField.Text, EmailField.Text, PhoneField.Text,
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
				SharedController.SaveButtonPress(NameField.Text, EmailField.Text, PhoneField.Text,
												 OldPasswordField.Text, NewPasswordField.Text, RepeatNewPasswordField.Text);
			} catch (Exception) {
				if (UserDetails.PasswordsChanged(NewPasswordField.Text, RepeatNewPasswordField.Text)) {
					NavigationController.ShowPrompt(Localizable.PromptMessages.InvalidPassword);
				} else {
					NavigationController.ShowPrompt(Localizable.PromptMessages.ChangesSaveError);
				}
				return;
			}

			SaveButton.Hidden = true;
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
			UserDetails SharedController {
				get {
					return Controller.SharedController;
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
				return SharedController.Requests == null ? 0 : SharedController.Requests.Length;
			}

			public override nfloat GetHeightForHeader(UITableView tableView, nint section)
			{
				return (nfloat)UserDetails.HeaderHeight;
			}

			public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
			{
				return (nfloat)UserDetails.RowHeight;
			}

			public override UIView GetViewForHeader(UITableView tableView, nint section)
			{
				var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, (nfloat)UserDetails.HeaderHeight));
				var Frame = new CGRect(10, 0, HeaderView.Frame.Width, (nfloat)UserDetails.HeaderHeight);
				var Label = new UILabel(Frame);

				HeaderView.BackgroundColor = UserDetails.TableHeaderColor.GetUIColor();
				Label.Text = UserDetails.HeaderTitle;
				if (SharedController.Requests != null) {
					Label.Text += " (" + SharedController.Requests.Length + ")";
				}
				HeaderView.AddSubview(Label);
				return HeaderView;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(RequestCell.Key, indexPath) as RequestCell;
				cell.Request = SharedController.Requests[indexPath.Row];
				cell.BackgroundColor = UserDetails.TableHeaderColor.GetUIColor().ColorWithAlpha((nfloat)UserDetails.RowAlphaDouble);
				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var controller = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.RequestDetails) as RequestDetailsController;
				Controller.NavigationController.PushViewController(controller, true);
				controller.Request = SharedController.Requests[indexPath.Row];
			}
		}
	}
}