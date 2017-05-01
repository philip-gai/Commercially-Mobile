using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Commercially.iOS
{
	public partial class RequestListController : UITableViewController
	{
		readonly RequestList SharedController = new RequestList();

		public RequestListController(IntPtr handle) : base(handle) { }

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
			TableView.Source = new RequestTableSource(this);
			SetButtons(NewButton);
		}

		partial void TopButtonTouchUpInside(UIButton sender)
		{
			CurrentType = GetRequestStatusType(sender);
			SetButtons(sender);
		}

		void SetButtons(UIButton activeButton)
		{
			var buttons = new UIButton[] { NewButton, AssignedButton, CompletedButton, CancelledButton };
			foreach (var button in buttons) {
				button.SetTitleColor(RequestList.InactiveTextColor.GetUIColor(), UIControlState.Normal);
				button.BackgroundColor = RequestList.GetTypeColor(GetRequestStatusType(button)).GetUIColor();
				button.Enabled = true;
			}
			activeButton.SetTitleColor(SharedController.CurrentTypeColor.GetUIColor(), UIControlState.Normal);
			activeButton.BackgroundColor = RequestList.ActiveBackgroundColor.GetUIColor();
			activeButton.Enabled = false;
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

		class RequestTableSource : UITableViewSource
		{
			readonly RequestListController Controller;
			RequestList SharedController {
				get {
					return Controller.SharedController;
				}
			}

			public RequestTableSource(RequestListController controller)
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
				return (nfloat)RequestList.HeaderHeight;
			}

			public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
			{
				return (nfloat)RequestList.RowHeight;
			}

			public override UIView GetViewForHeader(UITableView tableView, nint section)
			{
				var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, (nfloat)UserList.HeaderHeight));
				var Frame = new CGRect(10, 0, HeaderView.Frame.Width, (nfloat)UserList.HeaderHeight);
				var Label = new UILabel(Frame);

				HeaderView.BackgroundColor = SharedController.CurrentTypeColor.GetUIColor();
				Label.Text = SharedController.CurrentTypeTitle;
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
				cell.BackgroundColor = SharedController.CurrentTypeColor.GetUIColor().ColorWithAlpha((nfloat)RequestList.RowAlphaDouble);
				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var controller = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.RequestDetails) as RequestDetailsController;
				Controller.NavigationController.PushViewController(controller, true);
				controller.Request = SharedController.Requests[indexPath.Row];
			}

			public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
			{
				return Session.User.Type == UserRoleType.Admin;
			}

			public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
			{
				switch (editingStyle) {
					case UITableViewCellEditingStyle.Delete:
						RequestApi.DeleteRequest(SharedController.Requests[indexPath.Row]._id);
						break;
				}
				Controller.ViewWillAppear(false);
			}
		}
	}
}