// Created by Philip Gai

using Foundation;
using System;
using UIKit;
using CoreGraphics;

namespace Commercially.iOS
{
	/// <summary>
	/// Client list controller.
	/// </summary>
	public partial class ClientListController : UITableViewController
	{
		readonly ClientListManager Manager = new ClientListManager();

		public ClientListController(IntPtr handle) : base(handle) { }

		bool CurrentType {
			set {
				Manager.AuthorizedListType = value;
				GetClients();
			}
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			GetClients();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			TableView.Source = new ClientTableSource(this);
			SetButtons(AuthorizedButton);
		}

		partial void TopButtonTouchUpInside(UIButton sender)
		{
			CurrentType = GetClientListType(sender);
			SetButtons(sender);
		}

		void SetButtons(UIButton activeButton)
		{
			var buttons = new UIButton[] { AuthorizedButton, DiscoveredButton };
			foreach (var button in buttons) {
				button.SetTitleColor(ClientListManager.InactiveTextColor.GetUIColor(), UIControlState.Normal);
				button.BackgroundColor = ClientListManager.GetListTypeColor(GetClientListType(button)).GetUIColor();
				button.Enabled = true;
			}
			activeButton.SetTitleColor(Manager.CurrentListTypeColor.GetUIColor(), UIControlState.Normal);
			activeButton.BackgroundColor = ClientListManager.ActiveBackgroundColor.GetUIColor();
			activeButton.Enabled = false;
		}

		bool GetClientListType(UIButton sender)
		{
			return sender == AuthorizedButton;
		}

		void GetClients()
		{
			Manager.GetClients(delegate {
				InvokeOnMainThread(delegate {
					TableView.ReloadData();
				});
			}, (Exception e) => {
				InvokeOnMainThread(delegate {
					NavigationController.ShowPrompt(Localizable.PromptMessages.ClientsError);
				});
			});
		}

		class ClientTableSource : UITableViewSource
		{
			readonly ClientListController Controller;
			ClientListManager Manager {
				get {
					return Controller.Manager;
				}
			}

			public ClientTableSource(ClientListController controller)
			{
				Controller = controller;
				Controller.TableView.RegisterNibForCellReuse(UINib.FromName(ClientCell.Key, null), ClientCell.Key);
				Controller.TableView.RegisterNibForCellReuse(UINib.FromName(ButtonCell.Key, null), ButtonCell.Key);
			}

			public override nint NumberOfSections(UITableView tableView)
			{
				return 1;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return Manager.Clients == null ? 0 : Manager.Clients.Length;
			}

			public override nfloat GetHeightForHeader(UITableView tableView, nint section)
			{
				return (nfloat)ClientListManager.TableHeaderHeight;
			}

			public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
			{
				return (nfloat)ClientListManager.TableRowHeight;
			}

			public override UIView GetViewForHeader(UITableView tableView, nint section)
			{
				var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, (nfloat)ClientListManager.TableHeaderHeight));
				var Frame = new CGRect(10, 0, HeaderView.Frame.Width, (nfloat)ClientListManager.TableHeaderHeight);
				var Label = new UILabel(Frame);

				HeaderView.BackgroundColor = Manager.CurrentListTypeColor.GetUIColor();
				Label.Text = Manager.CurrentListTypeTitle;
				if (Manager.Clients != null) {
					Label.Text += " (" + Manager.Clients.Length + ")";
				}
				HeaderView.AddSubview(Label);
				return HeaderView;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(ClientCell.Key, indexPath) as ClientCell;
				cell.Client = Manager.Clients[indexPath.Row];
				cell.BackgroundColor = Manager.CurrentListTypeColor.GetUIColor().ColorWithAlpha((nfloat)ClientListManager.TableRowAlphaDouble);
				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var nextController = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.ClientDetails) as ClientDetailsController;
				Controller.NavigationController.PushViewController(nextController, true);
				nextController.Client = Manager.Clients[indexPath.Row];
			}
		}
	}
}