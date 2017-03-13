using Foundation;
using System;
using UIKit;

namespace Commercially.iOS
{
	public partial class RequestListController : UITableViewController
	{
		Request[] _Requests;
		public Request[] Requests {
			get {
				if (_Requests != null) return _Requests;
				_Requests = new Request[] { new Request("Room 101") };
				return _Requests;
				// Get requests from API and store them
			}
		}

		public RequestListController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			TableView.DataSource = new RequestListDataSource(this);
			TableView.Delegate = new RequestListDelegate(this);
		}
	}
}