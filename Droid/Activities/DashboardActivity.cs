
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
			Dashboard.GetRequests(InitializeTable, (obj) => { Console.WriteLine(obj.Message); });
		}

		void InitializeTable()
		{
			var table = FindViewById<TableLayout>(Resource.Id.tableLayout);
			var inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
			for (int section = 0; section < Dashboard.RequestLists.Length; section++) {
				var header = GetHeaderForSection(section);
				table.AddView(header);
				var requestList = Dashboard.RequestLists[section];
				for (int row = 0; row < requestList.Length; row++) {
					var tableRow = GetTableRow(row, section);
					table.AddView(tableRow, section);
				}
			}
		}

		View GetHeaderForSection(int section)
		{
			var inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
			var headerView = (TableRow)inflater.Inflate(Resource.Layout.RequestHeader, null);
			int arrayIndex = Dashboard.SectionToArray[section];
			headerView.SetBackgroundColor(Dashboard.SectionBackgroundColors[arrayIndex].GetAndroidColor());
			var headerLabel = headerView.FindViewById<TextView>(Resource.Id.headerText);
			headerLabel.Text = Dashboard.SectionTitles[arrayIndex];
			if (Dashboard.RequestLists != null) {
				headerLabel.Text += " (" + Dashboard.RequestLists[arrayIndex].Length + ")";
			}
			return headerView;
		}

		TableRow GetTableRow(int row, int section)
		{
			var inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
			int arrayIndex = Dashboard.SectionToArray[section];
			var requestList = Dashboard.RequestLists[arrayIndex];
			var request = requestList[row];
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
			return rowView;
		}
	}
}
