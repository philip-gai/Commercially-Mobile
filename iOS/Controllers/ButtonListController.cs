// Created by Philip Gai

using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// Button list controller.
	/// </summary>
	public partial class ButtonListController : UITableViewController
	{
		readonly ButtonListManager Manager = new ButtonListManager();

		public ButtonListController(IntPtr handle) : base(handle) { }

		ButtonType CurrentListType {
			set {
				Manager.CurrentListType = value;
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
			CurrentListType = GetButtonListType(sender);
			SetButtons(sender);
		}

		void SetButtons(UIButton activeButton)
		{
			var buttons = new UIButton[] { PairedButton, DiscoveredButton, IgnoredButton };
			foreach (var button in buttons) {
				button.SetTitleColor(ButtonListManager.InactiveTextColor.GetUIColor(), UIControlState.Normal);
				button.BackgroundColor = ButtonListManager.GetListTypeColor(GetButtonListType(button)).GetUIColor();
				button.Enabled = true;
			}
			activeButton.SetTitleColor(Manager.CurrentListTypeColor.GetUIColor(), UIControlState.Normal);
			activeButton.BackgroundColor = ButtonListManager.ActiveBackgroundColor.GetUIColor();
			activeButton.Enabled = false;
		}

		ButtonType GetButtonListType(UIButton sender)
		{
			if (sender == PairedButton) return ButtonType.Paired;
			if (sender == DiscoveredButton) return ButtonType.Discovered;
			if (sender == IgnoredButton) return ButtonType.Ignored;
			return ButtonType.Paired;
		}

		void GetButtons()
		{
			Manager.GetButtons(delegate {
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
			ButtonListManager Manager {
				get {
					return Controller.Manager;
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
				return Manager.Buttons == null ? 0 : Manager.Buttons.Length;
			}

			public override nfloat GetHeightForHeader(UITableView tableView, nint section)
			{
				return (nfloat)ButtonListManager.TableHeaderHeight;
			}

			public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
			{
				return (nfloat)ButtonListManager.TableRowHeight;
			}

			public override UIView GetViewForHeader(UITableView tableView, nint section)
			{
				var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, (nfloat)ButtonListManager.TableHeaderHeight));
				var Frame = new CGRect(10, 0, HeaderView.Frame.Width, (nfloat)ButtonListManager.TableHeaderHeight);
				var Label = new UILabel(Frame);

				HeaderView.BackgroundColor = Manager.CurrentListTypeColor.GetUIColor();
				Label.Text = Manager.CurrentListTypeTitle;
				if (Manager.Buttons != null) {
					Label.Text += " (" + Manager.Buttons.Length + ")";
				}
				HeaderView.AddSubview(Label);
				return HeaderView;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(ButtonCell.Key, indexPath) as ButtonCell;
				cell.Button = Manager.Buttons[indexPath.Row];
				cell.BackgroundColor = Manager.CurrentListTypeColor.GetUIColor().ColorWithAlpha((nfloat)ButtonListManager.TableRowAlphaDouble);
				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var nextController = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.ButtonDetails) as ButtonDetailsController;
				Controller.NavigationController.PushViewController(nextController, true);
				nextController.Button = Manager.Buttons[indexPath.Row];
			}
		}
	}
}