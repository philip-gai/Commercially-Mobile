// Created by Philip Gai

using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// User list controller.
	/// </summary>
	public partial class UserListController : UITableViewController
	{
		readonly UserListManager Manager = new UserListManager();

		public UserListController(IntPtr handle) : base(handle) { }

		UserRoleType CurrentListType {
			set {
				Manager.CurrentListType = value;
				GetUsers();
			}
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			GetUsers();
			var addItem = new UIBarButtonItem(UIBarButtonSystemItem.Add, (object sender, EventArgs e) => { NavigationController.GetAndActOnViewController(GlobalConstants.Screens.UserCreate); });
			TabBarController.NavigationItem.RightBarButtonItem = addItem;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			TableView.Source = new UserTableSource(this);
			SetButtons(WorkersButton);
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			TabBarController.NavigationItem.RightBarButtonItem = null;
		}

		partial void TopButtonTouchUpInside(UIButton sender)
		{
			CurrentListType = GetUserListType(sender);
			SetButtons(sender);
		}

		void SetButtons(UIButton activeButton)
		{
			var buttons = new UIButton[] { WorkersButton, TenantsButton, AdminsButton };
			foreach (var button in buttons) {
				button.SetTitleColor(UserListManager.InactiveTextColor.GetUIColor(), UIControlState.Normal);
				button.BackgroundColor = UserListManager.GetListTypeColor(GetUserListType(button)).GetUIColor();
				button.Enabled = true;
			}
			activeButton.SetTitleColor(Manager.CurrentListTypeColor.GetUIColor(), UIControlState.Normal);
			activeButton.BackgroundColor = UserListManager.ActiveBackgroundColor.GetUIColor();
			activeButton.Enabled = false;
		}

		UserRoleType GetUserListType(UIButton sender)
		{
			if (sender == WorkersButton) return UserRoleType.Worker;
			if (sender == TenantsButton) return UserRoleType.Tenant;
			if (sender == AdminsButton) return UserRoleType.Admin;
			return UserRoleType.Worker;
		}

		void GetUsers()
		{
			Manager.GetUsers(delegate {
				InvokeOnMainThread(delegate {
					TableView.ReloadData();
				});
			}, (Exception e) => {
				InvokeOnMainThread(delegate {
					NavigationController.ShowPrompt(Localizable.PromptMessages.UsersError);
				});
			});
		}

		class UserTableSource : UITableViewSource
		{
			readonly UserListController Controller;
			UserListManager Manager {
				get {
					return Controller.Manager;
				}
			}

			public UserTableSource(UserListController controller)
			{
				Controller = controller;
				Controller.TableView.RegisterNibForCellReuse(UINib.FromName(UserCell.Key, null), UserCell.Key);
			}

			public override nint NumberOfSections(UITableView tableView)
			{
				return 1;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return Manager.Users == null ? 0 : Manager.Users.Length;
			}

			public override nfloat GetHeightForHeader(UITableView tableView, nint section)
			{
				return (nfloat)UserListManager.TableHeaderHeight;
			}

			public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
			{
				return (nfloat)UserListManager.TableRowHeight;
			}

			public override UIView GetViewForHeader(UITableView tableView, nint section)
			{
				var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, (nfloat)UserListManager.TableHeaderHeight));
				var Frame = new CGRect(10, 0, HeaderView.Frame.Width, (nfloat)UserListManager.TableHeaderHeight);
				var Label = new UILabel(Frame);

				HeaderView.BackgroundColor = Manager.CurrentListTypeColor.GetUIColor();
				Label.Text = Manager.CurrentListTypeTitle;
				if (Manager.Users != null) {
					Label.Text += " (" + Manager.Users.Length + ")";
				}
				HeaderView.AddSubview(Label);
				return HeaderView;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(UserCell.Key, indexPath) as UserCell;
				cell.User = Manager.Users[indexPath.Row];
				cell.BackgroundColor = Manager.CurrentListTypeColor.GetUIColor().ColorWithAlpha((nfloat)UserListManager.TableRowAlphaDouble);
				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var nextController = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.UserDetails) as UserDetailsController;
				Controller.NavigationController.PushViewController(nextController, true);
				nextController.User = Manager.Users[indexPath.Row];
			}

			public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
			{
				return Manager.CanEditRow(indexPath.Row);
			}

			public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
			{
				switch (editingStyle) {
					case UITableViewCellEditingStyle.Delete:
						try {
							UserApi.DeleteUser(Manager.Users[indexPath.Row].id);
						} catch (Exception) {
							Controller.NavigationController.ShowPrompt(Localizable.PromptMessages.DeleteError);
							return;
						}
						break;
				}
				Controller.ViewWillAppear(false);
			}
		}
	}
}