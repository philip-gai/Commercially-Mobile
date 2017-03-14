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
		static string[] SectionTitles = { Localizable.Labels.InProgress, Localizable.Labels.ToDo, Localizable.Labels.Complete };
		static UIColor[] SectionBackgroundColors = { GlobalConstants.DefaultColors.Yellow.GetUIColor(), GlobalConstants.DefaultColors.Red.GetUIColor(), GlobalConstants.DefaultColors.Green.GetUIColor() };

		Request[][] RequestLists;

		public DashboardController(IntPtr handle) : base(handle) { }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			TableView.DataSource = this;
			TableView.Delegate = this;
			TableView.RegisterNibForCellReuse(UINib.FromName(LocalConstants.ReuseIdentifiers.RequestCell, null), LocalConstants.ReuseIdentifiers.RequestCell);

			new TaskFactory().StartNew(delegate {
				while (SessionData.Requests == null) { }
				RequestLists = SessionData.GetRequestLists();
				InvokeOnMainThread(() => {
					TableView.ReloadData();
				});
			});
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			if (RequestLists == null) return 0;
			int NumSections = 0;
			for (int i = 0; i < RequestLists.Length; i++) {
				if (RequestLists[i] != null && RequestLists[i].Length > 0) {
					NumSections++;
				}
			}
			return NumSections;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			if (RequestLists == null) return 0;
			int i;
			for (i = (int)section; i < RequestLists.Length && (RequestLists[i] == null || RequestLists[i].Length == 0); i++) { }
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
			var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, HeaderHeight));
			HeaderView.BackgroundColor = SectionBackgroundColors[section];

			var Frame = new CGRect(10, 0, HeaderView.Frame.Width, HeaderHeight);
			var Label = new UILabel(Frame);
			Label.Text = SectionTitles[section];
			if (RequestLists != null) {
				Label.Text += " (" + RequestLists[section].Length + ")";
			}
			HeaderView.AddSubview(Label);

			return HeaderView;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(LocalConstants.ReuseIdentifiers.RequestCell, indexPath) as RequestCell;
			cell.Request = RequestLists[indexPath.Section][indexPath.Row];
			cell.SetStatusLabelIsHidden(true);
			cell.BackgroundColor = SectionBackgroundColors[indexPath.Section].ColorWithAlpha((nfloat)0.33);
			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(LocalConstants.ReuseIdentifiers.RequestCell, indexPath) as RequestCell;
			var requestDetailsController = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.RequestDetails) as RequestDetailsController;
			requestDetailsController.Request = cell.Request;
			NavigationController.PushViewController(requestDetailsController, true);
		}
	}
}