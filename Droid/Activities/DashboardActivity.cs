
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Commercially.Droid
{
	[Activity(Label = "DashboardActivity")]
	public class DashboardActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Dashboard);

			Home.PrefetchData();
			Initialize();
		}

		void Initialize()
		{
			Dashboard.GetRequests(AddRows, (obj) => { Console.WriteLine(obj.Message); });
		}

		void AddRows()
		{
			var table = FindViewById<TableLayout>(Resource.Id.tableLayout);
			var inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
			for (int section = 0; section < Dashboard.RequestLists.Length; section++) {
				int arrayIndex = Dashboard.SectionToArray[section];
				var requestList = Dashboard.RequestLists[arrayIndex];
				var headerView = (TableRow)inflater.Inflate(Resource.Layout.RequestHeader, null);
				headerView.SetBackgroundColor(Dashboard.SectionBackgroundColors[arrayIndex].GetAndroidColor());
				var headerLabel = headerView.FindViewById<TextView>(Resource.Id.headerText);
				headerLabel.Text = Dashboard.SectionTitles[arrayIndex];
				if (Dashboard.RequestLists != null) {
					headerLabel.Text += " (" + Dashboard.RequestLists[arrayIndex].Length + ")";
				}
				for (int row = 0; row < requestList.Length; row++) {
					var request = requestList[arrayIndex];
					var rowView = (TableRow)inflater.Inflate(Resource.Layout.RequestRow, null);

					var description = rowView.FindViewById<TextView>(Resource.Id.descriptionText);
					var locationLabel = rowView.FindViewById<TextView>(Resource.Id.locationText);
					var timeLabel = rowView.FindViewById<TextView>(Resource.Id.timeText);
					var statusLabel = rowView.FindViewById<TextView>(Resource.Id.statusText);
					//var urgentIndicator = rowView.FindViewById<TextView>(Resource.Id.urgentIndicator);
					description.Text = request.description;
					locationLabel.Text = request.room;
					timeLabel.Text = request.GetTime(Request.TimeType.Received) ?? "N/A";
					statusLabel.Text = request.GetStatus().ToString();
					//urgentIndicator.visi = !request.urgent;

					table.AddView(rowView, section);
				}
			}
		}
	}
}
