using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;
using CoreGraphics;
using Commercially.iOS.Extensions;

namespace Commercially.iOS
{
	public partial class DashboardController : UITableViewController
	{
		static nfloat HeaderHeight = 50;
		static nfloat RowHeight = 88;
		static string[] SectionTitles = { Localizable.Labels.MyTasks, RequestStatusType.Completed.ToString(), RequestStatusType.Cancelled.ToString() };
		public static UIColor[] SectionBackgroundColors = { GlobalConstants.DefaultColors.Yellow.GetUIColor(), GlobalConstants.DefaultColors.Green.GetUIColor(), GlobalConstants.DefaultColors.Purple.GetUIColor() };

		Request[][] RequestLists;
		int[] SectionToArray = new int[SectionTitles.Length];

		public DashboardController(IntPtr handle) : base(handle) { }

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			SessionData.TaskFactory.StartNew(delegate {
				try {
					if (SessionData.TestMode) {
						SessionData.Requests = RequestApi.GetOfflineRequests();
						return;
					}
					SessionData.Requests = RequestApi.GetRequests();
					RequestLists = SessionData.GetRequestLists(new RequestStatusType[] { RequestStatusType.Assigned, RequestStatusType.Completed, RequestStatusType.Cancelled });
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

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			TableView.DataSource = this;
			TableView.Delegate = this;
			TableView.RegisterNibForCellReuse(UINib.FromName(LocalConstants.ReuseIdentifiers.RequestCell, null), LocalConstants.ReuseIdentifiers.RequestCell);
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			if (RequestLists == null) return 0;
			int NumSections = 0;
			for (int i = 0; i < RequestLists.Length; i++) {
				if (RequestLists[i] != null && RequestLists[i].Length > 0) {
					SectionToArray[NumSections] = i;
					NumSections++;
				}
			}
			return NumSections;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			if (RequestLists == null) return 0;
			int i;
			for (i = SectionToArray[section]; i < RequestLists.Length && (RequestLists[i] == null || RequestLists[i].Length == 0); i++) { }
			return RequestLists[i].Length;
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
			int arrayIndex = SectionToArray[section];
			var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, HeaderHeight));
			HeaderView.BackgroundColor = SectionBackgroundColors[arrayIndex];

			var Frame = new CGRect(10, 0, HeaderView.Frame.Width, HeaderHeight);
			var Label = new UILabel(Frame);
			Label.Text = SectionTitles[arrayIndex];
			if (RequestLists != null) {
				Label.Text += " (" + RequestLists[arrayIndex].Length + ")";
			}
			HeaderView.AddSubview(Label);

			return HeaderView;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			int arrayIndex = SectionToArray[indexPath.Section];
			var cell = tableView.DequeueReusableCell(LocalConstants.ReuseIdentifiers.RequestCell, indexPath) as RequestCell;
			cell.Request = RequestLists[arrayIndex][indexPath.Row];
			cell.SetStatusLabelIsHidden(true);
			cell.BackgroundColor = SectionBackgroundColors[arrayIndex].ColorWithAlpha((nfloat)0.33);
			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			int arrayIndex = SectionToArray[indexPath.Section];
			var requestDetailsController = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.RequestDetails) as RequestDetailsController;
			NavigationController.PushViewController(requestDetailsController, true);
			requestDetailsController.Request = RequestLists[arrayIndex][indexPath.Row];
		}
	}
}