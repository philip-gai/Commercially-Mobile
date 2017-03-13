using UIKit;
namespace Commercially.iOS
{
	public class RequestListDataSource : UITableViewDataSource
	{
		RequestListController Controller;

		public RequestListDataSource()
		{
		}

		public RequestListDataSource(RequestListController controller)
		{
			Controller = controller;
			Controller.TableView.RegisterNibForCellReuse(UINib.FromName(LocalConstants.ReuseIdentifiers.RequestCell, null), LocalConstants.ReuseIdentifiers.RequestCell);
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(LocalConstants.ReuseIdentifiers.RequestCell, indexPath) as RequestCell;
			int row = indexPath.Row;
			cell.Request = Controller.Requests[row];
			return cell;
		}

		public override System.nint RowsInSection(UITableView tableView, System.nint section)
		{
			return Controller.Requests.Length;
		}
	}
}
