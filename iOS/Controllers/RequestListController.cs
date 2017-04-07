using Foundation;
using System;
using UIKit;
using CoreGraphics;
using Commercially.iOS.Extensions;

namespace Commercially.iOS
{
	public partial class RequestListController : UITableViewController
	{
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
			RequestList.GetRequests(delegate {
				InvokeOnMainThread(delegate {
					TableView.ReloadData();
				});
			}, (Exception e) => {
				InvokeOnMainThread(delegate {
					NavigationController.ShowPrompt(e.Message, 50);
				});
			});
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return RequestList.NewRequestList == null ? 0 : RequestList.NewRequestList.Length == 0 ? 0 : 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return RequestList.NewRequestList == null ? 0 : RequestList.NewRequestList.Length;
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
			var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, RequestList.HeaderHeight));
			HeaderView.BackgroundColor = RequestList.TableBackgroundColor.GetUIColor();

			var Frame = new CGRect(10, 0, HeaderView.Frame.Width, RequestList.HeaderHeight);
			var Label = new UILabel(Frame);
			Label.Text = RequestStatusType.New.ToString();
			if (RequestList.NewRequestList != null) {
				Label.Text += " (" + RequestList.NewRequestList.Length + ")";
			}
			HeaderView.AddSubview(Label);
			return HeaderView;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(RequestCell.Key, indexPath) as RequestCell;
			cell.Request = RequestList.NewRequestList[indexPath.Row];
			cell.SetStatusLabelIsHidden(true);
			cell.BackgroundColor = RequestList.TableBackgroundColor.GetUIColor().ColorWithAlpha((nfloat)RequestList.RowAlphaDouble);
			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var requestDetailsController = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.RequestDetails) as RequestDetailsController;
			NavigationController.PushViewController(requestDetailsController, true);
			requestDetailsController.Request = RequestList.NewRequestList[indexPath.Row];
		}
	}
}