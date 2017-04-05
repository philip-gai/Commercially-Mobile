
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
			Dashboard.GetRequests(() => { RunOnUiThread(() => { InitializeTable(); }); }, (obj) => { Console.WriteLine(obj.Message); });
		}

		void InitializeTable()
		{
			var table = FindViewById<TableLayout>(Resource.Id.tableLayout);
			for (int section = 0; section < Dashboard.RequestLists.Length; section++) {
				var requestList = Dashboard.RequestLists[section];
				if (requestList.Length <= 0) continue;
				var header = GetHeader(section);
				table.AddView(header);
				for (int row = 0; row < requestList.Length; row++) {
					var tableRow = GetTableRow(row, section);
					table.AddView(tableRow);
				}
			}
		}

		View GetHeader(int section)
		{
			var inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
			var headerView = (TableRow)inflater.Inflate(Resource.Layout.RequestHeader, null);
			headerView.SetBackgroundColor(Dashboard.SectionBackgroundColors[section].GetAndroidColor());
			var headerLabel = headerView.FindViewById<TextView>(Resource.Id.headerText);
			headerLabel.Text = Dashboard.SectionTitles[section];
			if (Dashboard.RequestLists != null) {
				headerLabel.Text += " (" + Dashboard.RequestLists[section].Length + ")";
			}
			return headerView;
		}

		TableRow GetTableRow(int row, int section)
		{
			var inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
			var requestList = Dashboard.RequestLists[section];
			var request = requestList[row];
			var rowView = (TableRow)inflater.Inflate(Resource.Layout.RequestRow, null);
			Android.Graphics.Color color = Dashboard.SectionBackgroundColors[section].GetAndroidColor();
			color.A = (byte)Dashboard.RowAlpha;
			rowView.SetBackgroundColor(color);
			var description = rowView.FindViewById<TextView>(Resource.Id.descriptionText);
			var locationLabel = rowView.FindViewById<TextView>(Resource.Id.locationText);
			var timeLabel = rowView.FindViewById<TextView>(Resource.Id.timeText);
			var statusLabel = rowView.FindViewById<TextView>(Resource.Id.statusText);
			var urgentIndicator = rowView.FindViewById(Resource.Id.urgentIndicator);
			description.Text = request.description;
			locationLabel.Text = request.room;
			timeLabel.Text = request.GetTime(Request.TimeType.Received) ?? "N/A";
			statusLabel.Text = request.GetStatus().ToString();
			urgentIndicator.Visibility = request.urgent ? ViewStates.Visible : ViewStates.Gone;
			return rowView;
		}
	}
}
