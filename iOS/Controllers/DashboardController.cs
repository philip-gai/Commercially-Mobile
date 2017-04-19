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
			SharedController.GetRequests(Dashboard.RequestTypes, delegate {
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
			if (SharedController.RequestLists == null) return 0;
			int NumSections = 0;
			for (int i = 0; i < SharedController.RequestLists.Length; i++) {
				if (SharedController.RequestLists[i] != null && SharedController.RequestLists[i].Length > 0) {
					Dashboard.SectionToArray[NumSections] = i;
					NumSections++;
				}
			}
			return NumSections;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			if (SharedController.RequestLists == null) return 0;
			int i;
			for (i = Dashboard.SectionToArray[section]; i < SharedController.RequestLists.Length && (SharedController.RequestLists[i] == null || SharedController.RequestLists[i].Length == 0); i++) { }
			return SharedController.RequestLists[i].Length;
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
			int arrayIndex = Dashboard.SectionToArray[section];
			var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, Dashboard.HeaderHeight));
			HeaderView.BackgroundColor = Dashboard.SectionBackgroundColors[arrayIndex].GetUIColor();

			var Frame = new CGRect(10, 0, HeaderView.Frame.Width, Dashboard.HeaderHeight);
			var Label = new UILabel(Frame);
			Label.Text = Dashboard.SectionTitles[arrayIndex];
			if (SharedController.RequestLists != null) {
				Label.Text += " (" + SharedController.RequestLists[arrayIndex].Length + ")";
			}
			HeaderView.AddSubview(Label);

			return HeaderView;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			int arrayIndex = Dashboard.SectionToArray[indexPath.Section];
			var cell = tableView.DequeueReusableCell(RequestCell.Key, indexPath) as RequestCell;
			cell.Request = SharedController.RequestLists[arrayIndex][indexPath.Row];
			cell.SetStatusLabelIsHidden(true);
			cell.BackgroundColor = Dashboard.SectionBackgroundColors[arrayIndex].GetUIColor().ColorWithAlpha((nfloat)Dashboard.RowAlphaDouble);
			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			int arrayIndex = Dashboard.SectionToArray[indexPath.Section];
			var requestDetailsController = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.RequestDetails) as RequestDetailsController;
			NavigationController.PushViewController(requestDetailsController, true);
			requestDetailsController.Request = SharedController.RequestLists[arrayIndex][indexPath.Row];
		}
	}
}