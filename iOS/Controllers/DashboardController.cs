using Foundation;
using System;
using UIKit;
using Commercially.iOS.Extensions;

namespace Commercially.iOS
{
	public partial class DashboardController : UITableViewController
	{
		int NumSections = 3;
		int RowsInEachSection = 3;
		string[] SectionTitles = { Localizable.Labels.InProgress, Localizable.Labels.ToDo, Localizable.Labels.Complete };
		UIColor[] SectionBackgroundColors = { GlobalConstants.DefaultColors.Yellow.GetUIColor(), GlobalConstants.DefaultColors.Red.GetUIColor(), GlobalConstants.DefaultColors.Green.GetUIColor() };

		static Request[] InProgressRequests = { Request.GetDummyRequest() };
		static Request[] ToDoRequests = { Request.GetDummyRequest(), Request.GetDummyRequest(), Request.GetDummyRequest(), Request.GetDummyRequest() };
		static Request[] CompleteRequests = { Request.GetDummyRequest(), Request.GetDummyRequest(), Request.GetDummyRequest() };

		Request[][] Requests = { InProgressRequests, ToDoRequests, CompleteRequests };

		public DashboardController(IntPtr handle) : base(handle) { }

		public override void AwakeFromNib()
		{
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

		public override string TitleForHeader(UITableView tableView, nint section)
		{
			return SectionTitles[section];
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(LocalConstants.ReuseIdentifiers.RequestCell, indexPath) as RequestCell;
			cell.InitializeWithRequest(Requests[indexPath.Section][indexPath.Row]);
			cell.BackgroundColor = SectionBackgroundColors[indexPath.Section];
			return cell;
		}
	}
}