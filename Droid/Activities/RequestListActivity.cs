
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
	[Activity(Label = "RequestListActivity")]
	public class RequestListActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Table);

			ActionBar.Show();
			ActionBar.Title = "Queue";

			RequestList.GetRequests(() => { RunOnUiThread(() => { InitializeTable(); }); }, (obj) => { Console.WriteLine(obj.Message); });
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.top_menus, menu);
			menu.RemoveItem(Resource.Id.ListIcon);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			return base.OnOptionsItemSelected(item);
		}

		void InitializeTable()
		{
			var table = FindViewById<TableLayout>(Resource.Id.tableLayout);
			var header = GetHeader();
			table.AddView(header);
			for (int row = 0; row < RequestList.NewRequestList.Length; row++) {
				var tableRow = GetTableRow(row);
				table.AddView(tableRow);
			}
		}

		View GetHeader()
		{
			var inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
			var headerView = (TableRow)inflater.Inflate(Resource.Layout.RequestHeader, null);
			headerView.SetBackgroundColor(RequestList.TableBackgroundColor.GetAndroidColor());
			var headerLabel = headerView.FindViewById<TextView>(Resource.Id.headerText);
			headerLabel.Text = RequestStatusType.New.ToString();
			if (RequestList.NewRequestList != null) {
				headerLabel.Text += " (" + RequestList.NewRequestList.Length + ")";
			}
			return headerView;
		}

		TableRow GetTableRow(int row)
		{
			var inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
			var rowView = (TableRow)inflater.Inflate(Resource.Layout.RequestRow, null);
			Android.Graphics.Color color = RequestList.TableBackgroundColor.GetAndroidColor();
			color.A = RequestList.RowAlphaByte;
			rowView.SetBackgroundColor(color);
			var description = rowView.FindViewById<TextView>(Resource.Id.descriptionText);
			var locationLabel = rowView.FindViewById<TextView>(Resource.Id.locationText);
			var timeLabel = rowView.FindViewById<TextView>(Resource.Id.timeText);
			var statusLabel = rowView.FindViewById<TextView>(Resource.Id.statusText);
			var urgentIndicator = rowView.FindViewById(Resource.Id.urgentIndicator);

			var request = RequestList.NewRequestList[row];
			description.Text = request.description;
			locationLabel.Text = request.room;
			timeLabel.Text = request.GetTime(Request.TimeType.Received)?.ToShortTimeString();
			statusLabel.Text = request.GetStatus().ToString();
			urgentIndicator.Visibility = request.urgent ? ViewStates.Visible : ViewStates.Gone;
			return rowView;
		}
	}
}
