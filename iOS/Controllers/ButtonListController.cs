using System;
using CoreGraphics;
using Foundation;
using UIKit;

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
			TableView.Source = new ButtonTableSource(this);
			SetButtons(PairedButton);
		}

		partial void TopButtonTouchUpInside(UIButton sender)
		{
			CurrentType = GetButtonType(sender);
			SetButtons(sender);
		}

		void SetButtons(UIButton activeButton)
		{
			var buttons = new UIButton[] { PairedButton, DiscoveredButton, IgnoredButton };
			foreach (var button in buttons) {
				button.SetTitleColor(ButtonList.InactiveTextColor.GetUIColor(), UIControlState.Normal);
				button.BackgroundColor = ButtonList.GetTypeColor(GetButtonType(button)).GetUIColor();
				button.Enabled = true;
			}
			activeButton.SetTitleColor(SharedController.CurrentTypeColor.GetUIColor(), UIControlState.Normal);
			activeButton.BackgroundColor = ButtonList.ActiveBackgroundColor.GetUIColor();
			activeButton.Enabled = false;
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

		class ButtonTableSource : UITableViewSource
		{
			readonly ButtonListController Controller;
			ButtonList SharedController {
				get {
					return Controller.SharedController;
				}
			}

			public ButtonTableSource(ButtonListController controller)
			{
				Controller = controller;
				Controller.TableView.RegisterNibForCellReuse(UINib.FromName(ButtonCell.Key, null), ButtonCell.Key);
			}

			public override nint NumberOfSections(UITableView tableView)
			{
				return 1;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
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
				var Frame = new CGRect(10, 0, HeaderView.Frame.Width, (nfloat)ButtonList.HeaderHeight);
				var Label = new UILabel(Frame);

				HeaderView.BackgroundColor = SharedController.CurrentTypeColor.GetUIColor();
				Label.Text = SharedController.CurrentTypeTitle;
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
				cell.BackgroundColor = SharedController.CurrentTypeColor.GetUIColor().ColorWithAlpha((nfloat)ButtonList.RowAlphaDouble);
				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var controller = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.ButtonDetails) as ButtonDetailsController;
				Controller.NavigationController.PushViewController(controller, true);
				controller.Button = SharedController.Buttons[indexPath.Row];
			}
		}
	}
}