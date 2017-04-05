using Foundation;
using System;
using UIKit;
using CoreGraphics;
using Commercially.iOS.Extensions;

namespace Commercially.iOS
{
	public partial class RequestListController : UITableViewController
	{
		static nfloat HeaderHeight = 50;
		static nfloat RowHeight = 88;

		Request[] NewRequestList;

		public RequestListController(IntPtr handle) : base(handle) { }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			TableView.DataSource = this;
			TableView.Delegate = this;
			TableView.RegisterNibForCellReuse(UINib.FromName(RequestCell.Key, null), RequestCell.Key);
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			Session.TaskFactory.StartNew(delegate {
				try {
					Session.Requests = RequestApi.GetRequests();
					NewRequestList = Request.GetRequestLists(Session.Requests, new RequestStatusType[] { RequestStatusType.New })[0];
					InvokeOnMainThread(delegate {
						TableView.ReloadData();
					});
				} catch (Exception e) {
					InvokeOnMainThread(delegate {
						NavigationController.ShowPrompt(e.Message, 50);
					});
				}
			});
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return NewRequestList == null ? 0 : NewRequestList.Length == 0 ? 0 : 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return NewRequestList == null ? 0 : NewRequestList.Length;
		}

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			return HeaderHeight;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return RowHeight;
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, HeaderHeight));
			HeaderView.BackgroundColor = GlobalConstants.DefaultColors.Red.GetUIColor();

			var Frame = new CGRect(10, 0, HeaderView.Frame.Width, HeaderHeight);
			var Label = new UILabel(Frame);
			Label.Text = RequestStatusType.New.ToString();
			if (NewRequestList != null) {
				Label.Text += " (" + NewRequestList.Length + ")";
			}
			HeaderView.AddSubview(Label);
			return HeaderView;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(RequestCell.Key, indexPath) as RequestCell;
			cell.Request = NewRequestList[indexPath.Row];
			cell.SetStatusLabelIsHidden(true);
			cell.BackgroundColor = GlobalConstants.DefaultColors.Red.GetUIColor().ColorWithAlpha((nfloat)0.33);
			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var requestDetailsController = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.RequestDetails) as RequestDetailsController;
			NavigationController.PushViewController(requestDetailsController, true);
			requestDetailsController.Request = NewRequestList[indexPath.Row];
		}
	}
}