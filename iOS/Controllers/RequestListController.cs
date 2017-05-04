// Created by Philip Gai

using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// Request list controller.
	/// </summary>
	public partial class RequestListController : UITableViewController
	{
		readonly RequestListManager Manager = new RequestListManager();

		public RequestListController(IntPtr handle) : base(handle) { }

		RequestStatusType CurrentType {
			set {
				Manager.CurrentListType = value;
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
			CurrentType = GetRequestListType(sender);
			SetButtons(sender);
		}

		void SetButtons(UIButton activeButton)
		{
			var buttons = new UIButton[] { NewButton, AssignedButton, CompletedButton, CancelledButton };
			foreach (var button in buttons) {
				button.SetTitleColor(RequestListManager.InactiveTextColor.GetUIColor(), UIControlState.Normal);
				button.BackgroundColor = RequestListManager.GetListTypeColor(GetRequestListType(button)).GetUIColor();
				button.Enabled = true;
			}
			activeButton.SetTitleColor(Manager.CurrentListTypeColor.GetUIColor(), UIControlState.Normal);
			activeButton.BackgroundColor = RequestListManager.ActiveBackgroundColor.GetUIColor();
			activeButton.Enabled = false;
		}

		RequestStatusType GetRequestListType(UIButton sender)
		{
			if (sender == NewButton) return RequestStatusType.New;
			if (sender == AssignedButton) return RequestStatusType.Assigned;
			if (sender == CompletedButton) return RequestStatusType.Completed;
			if (sender == CancelledButton) return RequestStatusType.Cancelled;
			return RequestStatusType.New;
		}

		void GetRequests()
		{
			Manager.GetRequests(
				delegate { InvokeOnMainThread(delegate { TableView.ReloadData(); }); },
				(Exception obj) => { InvokeOnMainThread(delegate { NavigationController.ShowPrompt(Localizable.PromptMessages.RequestsError); }); }
			);
		}

		class RequestTableSource : UITableViewSource
		{
			readonly RequestListController Controller;
			RequestListManager Manager {
				get {
					return Controller.Manager;
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
				return Manager.Requests == null ? 0 : Manager.Requests.Length;
			}

			public override nfloat GetHeightForHeader(UITableView tableView, nint section)
			{
				return (nfloat)RequestListManager.TableHeaderHeight;
			}

			public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
			{
				return (nfloat)RequestListManager.TableRowHeight;
			}

			public override UIView GetViewForHeader(UITableView tableView, nint section)
			{
				var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, (nfloat)RequestListManager.TableHeaderHeight));
				var Frame = new CGRect(10, 0, HeaderView.Frame.Width, (nfloat)RequestListManager.TableHeaderHeight);
				var Label = new UILabel(Frame);

				HeaderView.BackgroundColor = Manager.CurrentListTypeColor.GetUIColor();
				Label.Text = Manager.CurrentListTypeTitle;
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
				cell.BackgroundColor = Manager.CurrentListTypeColor.GetUIColor().ColorWithAlpha((nfloat)RequestListManager.TableRowAlphaDouble);
				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var nextController = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.RequestDetails) as RequestDetailsController;
				Controller.NavigationController.PushViewController(nextController, true);
				nextController.Request = Manager.Requests[indexPath.Row];
			}

			public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
			{
				return Session.User.Type == UserRoleType.Admin;
			}

			public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
			{
				switch (editingStyle) {
					case UITableViewCellEditingStyle.Delete:
						try {
							RequestApi.DeleteRequest(Manager.Requests[indexPath.Row]._id);
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