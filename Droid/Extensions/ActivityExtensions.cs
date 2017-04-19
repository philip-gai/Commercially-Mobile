﻿using Android.App;
using Android.Content;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

using System;

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
				intent.PutExtra(typeof(Request).Name, JsonConvert.SerializeObject(request));
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
