using Foundation;
using System;
using UIKit;
using CoreGraphics;

namespace Commercially.iOS
{
	public partial class UserDetailsController : UITableViewController
	{
		readonly UserDetails SharedController = new UserDetails();

		public User User {
			set {
				SharedController.User = value;
			}
		}

		public UserDetailsController(IntPtr handle) : base(handle) { }

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			GetRequests();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			InitializeView();
		}

		void InitializeView()
		{
			if (SharedController.User == null) return;
			TableView.Source = new UserRequestTableSource(this);
			FirstLastLabel.Text = SharedController.FirstLastText;
			EmailLabel.Text = SharedController.EmailText;
			PhoneLabel.Text = SharedController.PhoneText;
			PhoneLabel.Hidden = SharedController.PhoneLabelIsHidden;
		}

		void GetRequests()
		{
			SharedController.GetRequests(delegate {
				InvokeOnMainThread(delegate {
					TableView.ReloadData();
				});
			}, (Exception e) => {
				InvokeOnMainThread(delegate {
					NavigationController.ShowPrompt(Localizable.PromptMessages.RequestsError);
				});
			});
		}

		class UserRequestTableSource : UITableViewSource
		{
			readonly UserDetailsController Controller;
			UserDetails SharedController {
				get {
					return Controller.SharedController;
				}
			}

			public UserRequestTableSource(UserDetailsController controller)
			{
				Controller = controller;
				Controller.TableView.RegisterNibForCellReuse(UINib.FromName(RequestCell.Key, null), RequestCell.Key);
			}

			public override nint NumberOfSections(UITableView tableView)
			{
				return 1;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return SharedController.Requests == null ? 0 : SharedController.Requests.Length;
			}

			public override nfloat GetHeightForHeader(UITableView tableView, nint section)
			{
				return (nfloat)UserDetails.HeaderHeight;
			}

			public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
			{
				return (nfloat)UserDetails.RowHeight;
			}

			public override UIView GetViewForHeader(UITableView tableView, nint section)
			{
				var HeaderView = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, (nfloat)UserDetails.HeaderHeight));
				var Frame = new CGRect(10, 0, HeaderView.Frame.Width, (nfloat)UserDetails.HeaderHeight);
				var Label = new UILabel(Frame);

				HeaderView.BackgroundColor = UserDetails.TableHeaderColor.GetUIColor();
				Label.Text = UserDetails.HeaderTitle;
				if (SharedController.Requests != null) {
					Label.Text += " (" + SharedController.Requests.Length + ")";
				}
				HeaderView.AddSubview(Label);
				return HeaderView;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(RequestCell.Key, indexPath) as RequestCell;
				cell.Request = SharedController.Requests[indexPath.Row];
				cell.BackgroundColor = UserDetails.TableHeaderColor.GetUIColor().ColorWithAlpha((nfloat)UserDetails.RowAlphaDouble);
				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var controller = UINavigationControllerExtensions.GetViewController(GlobalConstants.Screens.RequestDetails) as RequestDetailsController;
				Controller.NavigationController.PushViewController(controller, true);
				controller.Request = SharedController.Requests[indexPath.Row];
			}
		}
	}
}