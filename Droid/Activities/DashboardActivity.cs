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
			string label = Dashboard.SectionTitles[section];
			if (sharedController.RequestLists != null) {
				label += " (" + sharedController.RequestLists[section].Length + ")";
			}
			return this.GetSectionHeader(label);
		}

		TableRow GetTableRow(int row, int section)
		{
			var rowView = this.GetRequestRow(sharedController.RequestLists[section][row]);
			Android.Graphics.Color color = Dashboard.SectionBackgroundColors[section].GetAndroidColor();
			color.A = Dashboard.RowAlphaByte;
			rowView.SetBackgroundColor(color);
			return rowView;
		}
	}
}
