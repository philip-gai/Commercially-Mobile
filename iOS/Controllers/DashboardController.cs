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

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			SharedController.GetRequests(Dashboard.StartType, delegate {
				InvokeOnMainThread(delegate {
					TableView.ReloadData();
				});
			}, (Exception) => {
				InvokeOnMainThread(delegate {
					NavigationController.ShowPrompt(Localizable.PromptMessages.RequestsError);
				});
			});
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			TableView.DataSource = this;
			TableView.Delegate = this;
			TableView.RegisterNibForCellReuse(UINib.FromName(RequestCell.Key, null), RequestCell.Key);
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return SharedController.RequestList == null ? 0 : SharedController.RequestList.Length == 0 ? 0 : 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return SharedController.RequestList == null ? 0 : SharedController.RequestList.Length;
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
			HeaderView.BackgroundColor = Dashboard.SectionBackgroundColors[section].GetUIColor();

			var Frame = new CGRect(10, 0, HeaderView.Frame.Width, Dashboard.HeaderHeight);
			var Label = new UILabel(Frame);
			Label.Text = Dashboard.SectionTitles[section];
			if (SharedController.RequestList != null) {
				Label.Text += " (" + SharedController.RequestList.Length + ")";
			}
			HeaderView.AddSubview(Label);

			return HeaderView;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(RequestCell.Key, indexPath) as RequestCell;
			cell.Request = SharedController.RequestList[indexPath.Row];
			cell.SetStatusLabelIsHidden(true);
			cell.BackgroundColor = Dashboard.SectionBackgroundColors[indexPath.Section].GetUIColor().ColorWithAlpha((nfloat)Dashboard.RowAlphaDouble);
			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var requestDetailsController = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.RequestDetails) as RequestDetailsController;
			NavigationController.PushViewController(requestDetailsController, true);
			requestDetailsController.Request = SharedController.RequestList[indexPath.Row];
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return Session.User.GetUserRoleType() == UserRoleType.Admin;
		}

		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			switch (editingStyle) {
				case UITableViewCellEditingStyle.Delete:
					RequestApi.DeleteRequest(SharedController.RequestList[indexPath.Row]._id);
					break;
			}
			ViewWillAppear(false);
			ViewDidLoad();
		}

		partial void NewButtonTouchUpInside(UIButton sender)
		{
			throw new NotImplementedException();
		}

		partial void AssignedButtonTouchUpInside(UIButton sender)
		{
			throw new NotImplementedException();
		}

		partial void CompletedButtonTouchUpInside(UIButton sender)
		{
			throw new NotImplementedException();

		}

		partial void CancelledButtonTouchUpInside(UIButton sender)
		{
			throw new NotImplementedException();
		}
	}
}