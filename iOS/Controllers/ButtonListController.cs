using Foundation;
using System;
using UIKit;
using CoreGraphics;
using Commercially.iOS.Extensions;

namespace Commercially.iOS
{
	public partial class ButtonListController : UITableViewController
	{
		readonly ButtonList SharedController = new ButtonList();

		public ButtonListController(IntPtr handle) : base(handle) { }

		ButtonType CurrentType {
			set {
				SharedController.CurrentType = value;
				GetButtons();
			}
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			GetButtons();
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
			return SharedController.Buttons == null ? 0 : SharedController.Buttons.Length == 0 ? 0 : 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return SharedController.Buttons == null ? 0 : SharedController.Buttons.Length;
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
			Label.Text = SharedController.SectionTitle;
			if (SharedController.Buttons != null) {
				Label.Text += " (" + SharedController.Buttons.Length + ")";
			}
			HeaderView.AddSubview(Label);
			return HeaderView;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(ButtonCell.Key, indexPath) as ButtonCell;
			cell.Button = SharedController.Buttons[indexPath.Row];
			cell.BackgroundColor = ButtonList.TableBackgroundColor.GetUIColor().ColorWithAlpha((nfloat)ButtonList.RowAlphaDouble);
			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var buttonDetailsController = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.ButtonDetails) as ButtonDetailsController;
			NavigationController.PushViewController(buttonDetailsController, true);
			buttonDetailsController.Button = SharedController.Buttons[indexPath.Row];
		}

		partial void TopButtonTouchUpInside(UIButton sender)
		{
			CurrentType = GetButtonType(sender);
			var buttons = new UIButton[] { PairedButton, DiscoveredButton, IgnoredButton };
			foreach (var button in buttons) {
				button.SetTitleColor(ButtonList.InactiveColor.GetUIColor(), UIControlState.Normal);
				button.Enabled = true;
			}
			sender.SetTitleColor(ButtonList.ActiveColor.GetUIColor(), UIControlState.Normal);
			sender.Enabled = false;
		}

		ButtonType GetButtonType(UIButton sender)
		{
			if (sender == PairedButton) return ButtonType.Paired;
			if (sender == DiscoveredButton) return ButtonType.Discovered;
			if (sender == IgnoredButton) return ButtonType.Ignored;
			return ButtonType.Paired;
		}

		void GetButtons()
		{
			SharedController.GetButtons(delegate {
				InvokeOnMainThread(delegate {
					TableView.ReloadData();
				});
			}, (Exception e) => {
				InvokeOnMainThread(delegate {
					NavigationController.ShowPrompt(Localizable.PromptMessages.ButtonsError);
				});
			});
		}
	}
}