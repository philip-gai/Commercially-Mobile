using Android.App;
using Android.Content;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using Java.IO;

using System;
using Android.OS;

namespace Commercially.Droid
{
	public static class ActivityExtensions
	{
		public static void StartActivityMenuItem(this Activity activity, IMenuItem item)
		{
			switch (item.ItemId) {
				case Resource.Id.DashboardIcon:
					activity.StartActivity(new Intent(activity, typeof(DashboardActivity)));
					break;
				case Resource.Id.ListIcon:
					activity.StartActivity(new Intent(activity, typeof(RequestListActivity)));
					break;
				case Resource.Id.ButtonIcon:
					activity.StartActivity(new Intent(activity, typeof(ButtonListActivity)));
					break;
			}
		}

		public static void ShowPrompt(this Activity activity, string message)
		{
			var newFragment = new PromptDialogFragment(message);
			newFragment.Show(activity.FragmentManager, message);
		}

		public static View GetSectionHeader(this Activity activity, string label)
		{
			var inflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService);
			var headerView = (TableRow)inflater.Inflate(Resource.Layout.TableSectionHeader, null);
			headerView.SetBackgroundColor(RequestList.TableBackgroundColor.GetAndroidColor());
			var headerLabel = headerView.FindViewById<TextView>(Resource.Id.headerText);
			headerLabel.Text = label;
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
			description.Text = request.description;
			locationLabel.Text = request.room;
			timeLabel.Text = request.GetTime(Request.TimeType.Received)?.ToShortTimeString();
			statusLabel.Text = request.GetStatus().ToString();
			urgentIndicator.Visibility = request.urgent ? ViewStates.Visible : ViewStates.Gone;
			rowView.Click += (object sender, EventArgs e) => {
				var intent = new Intent(activity, typeof(RequestDetailsActivity));
				//var bundle = new Bundle();
				//bundle.PutSerializable(typeof(Request).Name, request);
				//intent.PutExtra(typeof(Request).Name, request);
				activity.StartActivity(intent);
			};
			return rowView;
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
			return rowView;
		}

		public static void SetRequestDetails(this Activity activity, Request request)
		{
			var descriptionText = activity.FindViewById<TextView>(Resource.Id.descriptionText);
			var urgentIndicator = activity.FindViewById(Resource.Id.urgentIndicator);
			var locationText = activity.FindViewById<TextView>(Resource.Id.locationText);
			var assignedToText = activity.FindViewById<TextView>(Resource.Id.assignedToText);
			var receivedTimeText = activity.FindViewById<TextView>(Resource.Id.receivedTimeText);
			var acceptedTimeText = activity.FindViewById<TextView>(Resource.Id.acceptedTimeText);
			var completedTimeText = activity.FindViewById<TextView>(Resource.Id.completedTimeText);
			var staticStatusText = activity.FindViewById<TextView>(Resource.Id.staticStatusText);
			var statusText = activity.FindViewById<TextView>(Resource.Id.statusText);
			var statusSpinner = activity.FindViewById<Spinner>(Resource.Id.statusSpinner);
			var assignButton = activity.FindViewById<Button>(Resource.Id.assignButton);
			var saveButton = activity.FindViewById<Button>(Resource.Id.saveButton);

			descriptionText.Text = request.description;
			urgentIndicator.Visibility = request.urgent ? ViewStates.Visible : ViewStates.Gone;
			locationText.Text = "Location: " + request.room;
			statusText.Text = request.GetStatus().ToString();
			assignedToText.Text = request.assignedTo;
			assignedToText.Visibility = string.IsNullOrWhiteSpace(request.assignedTo) ? ViewStates.Gone : ViewStates.Visible;
			receivedTimeText.Text = "Received:\n" + request.GetTime(Request.TimeType.Received) ?? "N/A";
			acceptedTimeText.Text = "Scheduled:\n" + request.GetTime(Request.TimeType.Scheduled) ?? "N/A";
			completedTimeText.Text = "Completed:\n" + request.GetTime(Request.TimeType.Completed) ?? "N/A";
		}

		public static void SetSupportActionBarDefault(this AppCompatActivity activity, string title)
		{
			activity.SupportActionBar.Show();
			activity.SupportActionBar.Title = "  " + title;
			activity.SupportActionBar.SetDisplayShowHomeEnabled(true);
			activity.SupportActionBar.SetIcon(Resource.Drawable.LogoRed);
			activity.SupportActionBar.SetDisplayShowTitleEnabled(true);
		}
	}
}
