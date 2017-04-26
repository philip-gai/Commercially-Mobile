using Foundation;
using System;
using UIKit;
using CoreGraphics;
using Commercially.iOS.Extensions;

namespace Commercially.iOS
{
	public partial class DashboardController : UITableViewController
	{
		readonly Dashboard SharedController = new Dashboard();

		public DashboardController(IntPtr handle) : base(handle) { }

		RequestStatusType CurrentType {
			set {
				SharedController.CurrentType = value;
				GetRequests();
			}
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			GetRequests();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			TableView.DataSource = this;
			TableView.Delegate = this;
			TableView.RegisterNibForCellReuse(UINib.FromName(RequestCell.Key, null), RequestCell.Key);
			NewButton.Enabled = false;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return SharedController.Requests == null ? 0 : SharedController.Requests.Length == 0 ? 0 : 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return SharedController.Requests == null ? 0 : SharedController.Requests.Length;
		}

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			return (nfloat)Dashboard.HeaderHeight;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return (nfloat)Dashboard.RowHeight;
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, Dashboard.HeaderHeight));
			HeaderView.BackgroundColor = SharedController.SectionColor.GetUIColor();

			var Frame = new CGRect(10, 0, HeaderView.Frame.Width, Dashboard.HeaderHeight);
			var Label = new UILabel(Frame);
			Label.Text = SharedController.SectionTitle;
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
			cell.BackgroundColor = SharedController.SectionColor.GetUIColor().ColorWithAlpha((nfloat)Dashboard.RowAlphaDouble);
			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var requestDetailsController = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.RequestDetails) as RequestDetailsController;
			NavigationController.PushViewController(requestDetailsController, true);
			requestDetailsController.Request = SharedController.Requests[indexPath.Row];
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return Session.User.GetUserRoleType() == UserRoleType.Admin;
		}

		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			switch (editingStyle) {
				case UITableViewCellEditingStyle.Delete:
					RequestApi.DeleteRequest(SharedController.Requests[indexPath.Row]._id);
					break;
			}
			ViewWillAppear(false);
		}

		partial void TopButtonTouchUpInside(UIButton sender)
		{
			CurrentType = GetRequestStatusType(sender);
			var buttons = new UIButton[] { NewButton, AssignedButton, CompletedButton, CancelledButton };
			foreach (var button in buttons) {
				button.SetTitleColor(Dashboard.InactiveColor.GetUIColor(), UIControlState.Normal);
				button.Enabled = true;
			}
			sender.SetTitleColor(SharedController.SectionColor.GetUIColor(), UIControlState.Normal);
			sender.Enabled = false;
		}

		RequestStatusType GetRequestStatusType(UIButton sender)
		{
			if (sender == NewButton) return RequestStatusType.New;
			if (sender == AssignedButton) return RequestStatusType.Assigned;
			if (sender == CompletedButton) return RequestStatusType.Completed;
			if (sender == CancelledButton) return RequestStatusType.Cancelled;
			return RequestStatusType.New;
		}

		void GetRequests()
		{
			SharedController.GetRequests(
				delegate { InvokeOnMainThread(delegate { TableView.ReloadData(); }); },
				(Exception obj) => { InvokeOnMainThread(delegate { NavigationController.ShowPrompt(Localizable.PromptMessages.RequestsError); }); }
			);
		}
	}
}