using System;

using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Commercially.Droid
{
	[Activity(Label = "DashboardActivity")]
	public class DashboardActivity : AppCompatActivity
	{
		readonly Dashboard sharedController = new Dashboard();

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Table);
			SupportActionBar.Title = "Dashboard";
			Home.PrefetchData();
			sharedController.GetRequests(
				Dashboard.RequestTypes,
				delegate {
					RunOnUiThread(delegate {
						InitializeTable();
					});
				},
				(Exception obj) => {
					RunOnUiThread(delegate {
						this.ShowPrompt(Localizable.PromptMessages.RequestsError);
					});
				}
			);
		}


		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.TopMenu, menu);
			menu.RemoveItem(Resource.Id.DashboardIcon);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			this.StartActivityMenuItem(item);
			return base.OnOptionsItemSelected(item);
		}

		void InitializeTable()
		{
			var table = FindViewById<TableLayout>(Resource.Id.tableLayout);
			for (int section = 0; section < sharedController.RequestLists.Length; section++) {
				var requestList = sharedController.RequestLists[section];
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
			if (sharedController.RequestLists != null) {
				headerLabel.Text += " (" + sharedController.RequestLists[section].Length + ")";
			}
			return headerView;
		}

		TableRow GetTableRow(int row, int section)
		{
			var inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
			var rowView = (TableRow)inflater.Inflate(Resource.Layout.RequestRow, null);
			Android.Graphics.Color color = Dashboard.SectionBackgroundColors[section].GetAndroidColor();
			color.A = Dashboard.RowAlphaByte;
			rowView.SetBackgroundColor(color);
			var description = rowView.FindViewById<TextView>(Resource.Id.descriptionText);
			var locationLabel = rowView.FindViewById<TextView>(Resource.Id.locationText);
			var timeLabel = rowView.FindViewById<TextView>(Resource.Id.timeText);
			var statusLabel = rowView.FindViewById<TextView>(Resource.Id.statusText);
			var urgentIndicator = rowView.FindViewById(Resource.Id.urgentIndicator);

			var requestList = sharedController.RequestLists[section];
			var request = requestList[row];
			description.Text = request.description;
			locationLabel.Text = request.room;
			timeLabel.Text = request.GetTime(Request.TimeType.Received)?.ToShortTimeString();
			statusLabel.Text = request.GetStatus().ToString();
			urgentIndicator.Visibility = request.urgent ? ViewStates.Visible : ViewStates.Gone;
			return rowView;
		}
	}
}
