using Foundation;
using System;
using UIKit;
using CoreGraphics;
using Commercially.iOS.Extensions;

namespace Commercially.iOS
{
	public partial class ButtonListController : UITableViewController
	{
		public ButtonListController(IntPtr handle) : base(handle) { }

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			ButtonList.GetButtons(delegate {
				InvokeOnMainThread(delegate {
					TableView.ReloadData();
				});
			}, (Exception e) => {
				InvokeOnMainThread(delegate {
					NavigationController.ShowPrompt(Localizable.PromptMessages.ButtonsError);
				});
			});

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			TableView.DataSource = this;
			TableView.Delegate = this;
			TableView.RegisterNibForCellReuse(UINib.FromName(ButtonCell.Key, null), ButtonCell.Key);
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return Session.Buttons == null ? 0 : Session.Buttons.Length == 0 ? 0 : 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return Session.Buttons == null ? 0 : Session.Buttons.Length;
		}

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			return (nfloat)ButtonList.HeaderHeight;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return (nfloat)ButtonList.RowHeight;
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, (nfloat)ButtonList.HeaderHeight));
			HeaderView.BackgroundColor = ButtonList.TableBackgroundColor.GetUIColor();

			var Frame = new CGRect(10, 0, HeaderView.Frame.Width, (nfloat)ButtonList.HeaderHeight);
			var Label = new UILabel(Frame);
			Label.Text = "Buttons";
			if (Session.Buttons != null) {
				Label.Text += " (" + Session.Buttons.Length + ")";
			}
			HeaderView.AddSubview(Label);
			return HeaderView;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(ButtonCell.Key, indexPath) as ButtonCell;
			cell.Button = Session.Buttons[indexPath.Row];
			cell.BackgroundColor = ButtonList.TableBackgroundColor.GetUIColor().ColorWithAlpha((nfloat)ButtonList.RowAlphaDouble);
			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var buttonDetailsController = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.ButtonDetails) as ButtonDetailsController;
			NavigationController.PushViewController(buttonDetailsController, true);
			buttonDetailsController.Button = Session.Buttons[indexPath.Row];
		}
	}
}