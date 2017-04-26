using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

using System;
using Android.Graphics;

namespace Commercially.Droid
{
	public static class ActivityExtensions
	{
		public static void StartActivityMenuItem(this Activity activity, IMenuItem item)
		{
			switch (item.ItemId) {
				case Resource.Id.DashboardIcon:
					if (activity is DashboardActivity) return;
					activity.StartActivity(new Intent(activity, typeof(DashboardActivity)));
					break;
				case Resource.Id.ButtonIcon:
					if (activity is ButtonListActivity) return;
					activity.StartActivity(new Intent(activity, typeof(ButtonListActivity)));
					break;
			}
		}

		public static void ShowPrompt(this Activity activity, string message)
		{
			var newFragment = new PromptDialogFragment(message);
			newFragment.Show(activity.FragmentManager, message);
		}

		public static TableRow GetSectionHeader(this Activity activity, string label)
		{
			var inflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService);
			var headerView = (TableRow)inflater.Inflate(Resource.Layout.TableSectionHeader, null);
			headerView.SetBackgroundColor(RequestList.TableBackgroundColor.GetAndroidColor());
			var headerLabel = headerView.FindViewById<TextView>(Resource.Id.headerText);
			headerLabel.Text = label;
			return headerView;
		}

		public static LinearLayout GetDashboardHeader(this Activity activity)
		{
			var inflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService);
			var headerView = (LinearLayout)inflater.Inflate(Resource.Layout.TableButtonHeader, null);

			headerView.RemoveAllViews();
			foreach (var type in Dashboard.SectionTypes) {
				var button = (Button)inflater.Inflate(Resource.Id.topButton, null);
				button.Text = type.ToString();
				headerView.AddView(button);
			}
			return headerView;
		}

		public static LinearLayout GetButtonListHeader(this Activity activity)
		{
			var inflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService);
			var headerView = (LinearLayout)inflater.Inflate(Resource.Layout.TableButtonHeader, null);

			headerView.RemoveAllViews();
			foreach (var type in ButtonList.ButtonTypes) {
				var button = (Button)inflater.Inflate(Resource.Id.topButton, null);
				button.Text = type.ToString();
				headerView.AddView(button);
			}
			return headerView;
		}

		public static TableRow GetRequestRow(this Activity activity, Request request)
		{
			var inflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService);
			var rowView = (TableRow)inflater.Inflate(Resource.Layout.RequestRow, null);
			var description = rowView.FindViewById<TextView>(Resource.Id.descriptionText);
			var locationLabel = rowView.FindViewById<TextView>(Resource.Id.locationText);
			var timeLabel = rowView.FindViewById<TextView>(Resource.Id.timeText);
			var statusLabel = rowView.FindViewById<TextView>(Resource.Id.statusText);
			var urgentIndicator = rowView.FindViewById(Resource.Id.urgentIndicator);
			var deleteButton = rowView.FindViewById<Button>(Resource.Id.deleteButton);
			description.Text = request.description;
			locationLabel.Text = request.room;
			timeLabel.Text = request.GetTime(Request.TimeType.Received)?.ToShortTimeString();
			statusLabel.Text = request.Type.ToString();
			urgentIndicator.Visibility = request.urgent ? ViewStates.Visible : ViewStates.Gone;
			deleteButton.Visibility = ViewStates.Gone;
			deleteButton.Click += (object sender, EventArgs e) => {
				RequestApi.DeleteRequest(request._id);
				var table = activity.FindViewById<TableLayout>(Resource.Id.tableLayout);
				table.RemoveView(rowView);
			};

			rowView.Click += (object sender, EventArgs e) => {
				var intent = new Intent(activity, typeof(RequestDetailsActivity));
				intent.PutExtra(typeof(Request).Name, JsonConvert.SerializeObject(request));
				activity.StartActivity(intent);
			};
			if (Session.User.GetUserRoleType() == UserRoleType.Admin) {
				rowView.LongClick += (object sender, View.LongClickEventArgs e) => {
					switch (deleteButton.Visibility) {
						case ViewStates.Gone:
							deleteButton.Visibility = ViewStates.Visible;
							break;
						case ViewStates.Visible:
							deleteButton.Visibility = ViewStates.Gone;
							break;
					}
				};
			}
			return rowView;
		}

		public static void HideRequestStatusLabel(this Activity activity, TableRow rowView)
		{
			var statusLabel = rowView.FindViewById<TextView>(Resource.Id.statusText);
			statusLabel.Visibility = ViewStates.Gone;
		}

		public static TableRow GetButtonRow(this Activity activity, FlicButton button)
		{
			var inflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService);
			var rowView = (TableRow)inflater.Inflate(Resource.Layout.ButtonRow, null);
			var buttonLabel = rowView.FindViewById<TextView>(Resource.Id.buttonText);
			var clientLabel = rowView.FindViewById<TextView>(Resource.Id.clientText);
			var descriptionLabel = rowView.FindViewById<TextView>(Resource.Id.descriptionText);
			var locationLabel = rowView.FindViewById<TextView>(Resource.Id.locationText);

			buttonLabel.Text = button.bluetooth_id;
			var tmpClient = Client.FindClient(button.clientId, Session.Clients);
			clientLabel.Text = tmpClient != null && tmpClient.friendlyName != null ? tmpClient.friendlyName : button.clientId;
			descriptionLabel.Text = button.description;
			locationLabel.Text = button.room;
			rowView.Click += (object sender, EventArgs e) => {
				var intent = new Intent(activity, typeof(ButtonDetailsActivity));
				intent.PutExtra(typeof(FlicButton).Name, JsonConvert.SerializeObject(button));
				activity.StartActivity(intent);
			};
			return rowView;
		}

		public static void InitializeStatusSpinner(this Activity activity)
		{
			Spinner statusSpinner = activity.FindViewById<Spinner>(Resource.Id.statusSpinner);
			var adapter = ArrayAdapter.CreateFromResource(activity, Resource.Array.status_array, Android.Resource.Layout.SimpleSpinnerDropDownItem);
			adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			statusSpinner.Adapter = adapter;
		}

		public static void InitializeClientSpinner(this Activity activity, FlicButton button)
		{
			Spinner statusSpinner = activity.FindViewById<Spinner>(Resource.Id.clientSpinner);
			string[] tmpDiscoveredBy = new string[button.discoveredBy.Length + 2];
			tmpDiscoveredBy[0] = Localizable.Labels.NoneOption;
			tmpDiscoveredBy[1] = GlobalConstants.Strings.Ignore;
			button.discoveredBy.CopyTo(tmpDiscoveredBy, 2);
			var adapter = new ArrayAdapter(activity, Android.Resource.Layout.SimpleSpinnerDropDownItem, tmpDiscoveredBy);
			adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
			statusSpinner.Adapter = adapter;
		}

		public static void CreateMainOptionsMenu(this Activity activity, IMenu menu, int currItem)
		{
			activity.MenuInflater.Inflate(Resource.Menu.TopMenu, menu);
			var rIds = new int[] { Resource.Id.DashboardIcon, Resource.Id.ButtonIcon };
			foreach (var id in rIds) {
				var item = menu.FindItem(id);
				if (id == currItem) {
					item.Icon.SetColorFilter(GlobalConstants.DefaultColors.Red.GetAndroidColor(), PorterDuff.Mode.SrcIn);
					item.SetEnabled(false);
				} else {
					item.Icon.SetColorFilter(GlobalConstants.DefaultColors.Black.GetAndroidColor(), PorterDuff.Mode.SrcIn);
					item.SetEnabled(true);
				}
			}
			if (Session.User.GetUserRoleType() != UserRoleType.Admin) {
				menu.RemoveGroup(Resource.Id.AdminGroup);
			}
		}
	}
}