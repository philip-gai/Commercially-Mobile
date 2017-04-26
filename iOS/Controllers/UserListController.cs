using Foundation;
using System;
using UIKit;
using Commercially.iOS.Extensions;
using CoreGraphics;

namespace Commercially.iOS
{
	public partial class UserListController : UITableViewController
	{
		readonly UserList SharedController = new UserList();

		public UserListController(IntPtr handle) : base(handle) { }

		UserRoleType CurrentType {
			set {
				SharedController.CurrentType = value;
				GetUsers();
			}
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			GetUsers();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			TableView.DataSource = this;
			TableView.Delegate = this;
			TableView.RegisterNibForCellReuse(UINib.FromName(UserCell.Key, null), UserCell.Key);
			SetButtons(WorkersButton);
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return SharedController.Users == null ? 0 : SharedController.Users.Length;
		}

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			return (nfloat)UserList.HeaderHeight;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return (nfloat)UserList.RowHeight;
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, (nfloat)UserList.HeaderHeight));
			HeaderView.BackgroundColor = SharedController.CurrentTypeColor.GetUIColor();

			var Frame = new CGRect(10, 0, HeaderView.Frame.Width, (nfloat)UserList.HeaderHeight);
			var Label = new UILabel(Frame);
			Label.Text = SharedController.CurrentTypeTitle;
			if (SharedController.Users != null) {
				Label.Text += " (" + SharedController.Users.Length + ")";
			}
			HeaderView.AddSubview(Label);
			return HeaderView;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(UserCell.Key, indexPath) as UserCell;
			cell.User = SharedController.Users[indexPath.Row];
			cell.BackgroundColor = SharedController.CurrentTypeColor.GetUIColor().ColorWithAlpha((nfloat)UserList.RowAlphaDouble);
			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			//var buttonDetailsController = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.ButtonDetails) as UserDetailsController;
			//NavigationController.PushViewController(buttonDetailsController, true);
			//buttonDetailsController.Button = SharedController.Users[indexPath.Row];
		}

		partial void TopButtonTouchUpInside(UIButton sender)
		{
			CurrentType = GetUserRoleType(sender);
			SetButtons(sender);
		}

		void SetButtons(UIButton activeButton)
		{
			var buttons = new UIButton[] { WorkersButton, TenantsButton, AdminsButton };
			foreach (var button in buttons) {
				button.SetTitleColor(UserList.InactiveTextColor.GetUIColor(), UIControlState.Normal);
				button.BackgroundColor = UserList.GetTypeColor(GetUserRoleType(button)).GetUIColor();
				button.Enabled = true;
			}
			activeButton.SetTitleColor(SharedController.CurrentTypeColor.GetUIColor(), UIControlState.Normal);
			activeButton.BackgroundColor = UserList.ActiveBackgroundColor.GetUIColor();
			activeButton.Enabled = false;
		}

		UserRoleType GetUserRoleType(UIButton sender)
		{
			if (sender == WorkersButton) return UserRoleType.Worker;
			if (sender == TenantsButton) return UserRoleType.Tenant;
			if (sender == AdminsButton) return UserRoleType.Admin;
			return UserRoleType.Worker;
		}

		void GetUsers()
		{
			SharedController.GetUsers(delegate {
				InvokeOnMainThread(delegate {
					TableView.ReloadData();
				});
			}, (Exception e) => {
				InvokeOnMainThread(delegate {
					NavigationController.ShowPrompt(Localizable.PromptMessages.UsersError);
				});
			});
		}
	}
}