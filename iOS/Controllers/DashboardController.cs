using Foundation;
using System;
using UIKit;
using CoreGraphics;
using Commercially.iOS.Extensions;

namespace Commercially.iOS
{
	public partial class DashboardController : UITableViewController
	{
		int NumSections = 3;
		int RowsInEachSection = 3;
		nfloat HeaderHeight = 50;
		nfloat RowHeight = 88;
		string[] SectionTitles = { Localizable.Labels.InProgress, Localizable.Labels.ToDo, Localizable.Labels.Complete };
		UIColor[] SectionBackgroundColors = { GlobalConstants.DefaultColors.Yellow.GetUIColor(), GlobalConstants.DefaultColors.Red.GetUIColor(), GlobalConstants.DefaultColors.Green.GetUIColor() };

		static Request[] InProgressRequests = { Request.GetDummyRequest() };
		static Request[] ToDoRequests = { Request.GetDummyRequest(), Request.GetDummyRequest(), Request.GetDummyRequest(), Request.GetDummyRequest() };
		static Request[] CompleteRequests = { Request.GetDummyRequest(), Request.GetDummyRequest(), Request.GetDummyRequest() };

		Request[][] Requests = { InProgressRequests, ToDoRequests, CompleteRequests };

		public DashboardController(IntPtr handle) : base(handle) { }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			TableView.DataSource = this;
			TableView.Delegate = this;
			TableView.RegisterNibForCellReuse(UINib.FromName(LocalConstants.ReuseIdentifiers.RequestCell, null), LocalConstants.ReuseIdentifiers.RequestCell);
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return NumSections;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return Requests[section].Length < RowsInEachSection ? Requests[section].Length : RowsInEachSection;
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
			Label.Text = SectionTitles[section] + " (" + Requests[section].Length + ")";
			HeaderView.AddSubview(Label);

			return HeaderView;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(LocalConstants.ReuseIdentifiers.RequestCell, indexPath) as RequestCell;
			cell.InitializeWithRequest(Requests[indexPath.Section][indexPath.Row]);
			cell.BackgroundColor = SectionBackgroundColors[indexPath.Section].ColorWithAlpha((nfloat)0.33);
			return cell;
		}
	}
}