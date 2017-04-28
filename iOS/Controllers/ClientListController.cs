using Foundation;
using System;
using UIKit;
using CoreGraphics;

namespace Commercially.iOS
{
	public partial class ClientListController : UITableViewController
	{
		readonly ClientList SharedController = new ClientList();

		public ClientListController(IntPtr handle) : base(handle) { }

		bool CurrentType {
			set {
				SharedController.AuthorizedType = value;
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
			TableView.Source = new ButtonTableSource(this);
			SetButtons(AuthorizedButton);
		}

		partial void TopButtonTouchUpInside(UIButton sender)
		{
			CurrentType = GetButtonType(sender);
			SetButtons(sender);
		}

		void SetButtons(UIButton activeButton)
		{
			var buttons = new UIButton[] { AuthorizedButton, DiscoveredButton };
			foreach (var button in buttons) {
				button.SetTitleColor(ClientList.InactiveTextColor.GetUIColor(), UIControlState.Normal);
				button.BackgroundColor = ClientList.GetTypeColor(GetButtonType(button)).GetUIColor();
				button.Enabled = true;
			}
			activeButton.SetTitleColor(SharedController.CurrentTypeColor.GetUIColor(), UIControlState.Normal);
			activeButton.BackgroundColor = ClientList.ActiveBackgroundColor.GetUIColor();
			activeButton.Enabled = false;
		}

		bool GetButtonType(UIButton sender)
		{
			return sender == AuthorizedButton;
		}

		void GetClients()
		{
			SharedController.GetClients(delegate {
				InvokeOnMainThread(delegate {
					TableView.ReloadData();
				});
			}, (Exception e) => {
				InvokeOnMainThread(delegate {
					NavigationController.ShowPrompt(Localizable.PromptMessages.ClientsError);
				});
			});
		}

		class ButtonTableSource : UITableViewSource
		{
			readonly ClientListController Controller;
			ClientList SharedController {
				get {
					return Controller.SharedController;
				}
			}

			public ButtonTableSource(ClientListController controller)
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
				return SharedController.Clients == null ? 0 : SharedController.Clients.Length;
			}

			public override nfloat GetHeightForHeader(UITableView tableView, nint section)
			{
				return (nfloat)ClientList.HeaderHeight;
			}

			public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
			{
				return (nfloat)ClientList.RowHeight;
			}

			public override UIView GetViewForHeader(UITableView tableView, nint section)
			{
				var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, (nfloat)ClientList.HeaderHeight));
				var Frame = new CGRect(10, 0, HeaderView.Frame.Width, (nfloat)ClientList.HeaderHeight);
				var Label = new UILabel(Frame);

				HeaderView.BackgroundColor = SharedController.CurrentTypeColor.GetUIColor();
				Label.Text = SharedController.CurrentTypeTitle;
				if (SharedController.Clients != null) {
					Label.Text += " (" + SharedController.Clients.Length + ")";
				}
				HeaderView.AddSubview(Label);
				return HeaderView;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(ClientCell.Key, indexPath) as ClientCell;
				cell.Client = SharedController.Clients[indexPath.Row];
				cell.BackgroundColor = SharedController.CurrentTypeColor.GetUIColor().ColorWithAlpha((nfloat)ClientList.RowAlphaDouble);
				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var nextController = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.ClientDetails) as ClientDetailsController;
				Controller.NavigationController.PushViewController(nextController, true);
				nextController.Client = SharedController.Clients[indexPath.Row];
			}
		}
	}
}