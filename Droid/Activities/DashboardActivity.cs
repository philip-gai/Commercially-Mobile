using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Graphics;
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

		TableLayout Table { get { return FindViewById<TableLayout>(Resource.Id.tableLayout); } }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Table);
			this.SetSupportActionBarDefault();
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

			Home.PrefetchData();
		}

		protected override void OnResume()
		{
			base.OnResume();
			sharedController.GetRequests(
				Dashboard.StartType,
				delegate { RunOnUiThread(delegate { InitializeTable(); }); },
				(Exception e) => { RunOnUiThread(delegate { this.ShowPrompt(Localizable.PromptMessages.RequestsError); }); }
			);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			this.CreateMainOptionsMenu(menu, Resource.Id.DashboardIcon);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			this.StartActivityMenuItem(item);
			return base.OnOptionsItemSelected(item);
		}

		void InitializeTable()
		{
			Table.RemoveAllViews();
			var header = GetHeader(0);
			Table.AddView(header);
			for (int row = 0; row < sharedController.RequestList.Length; row++) {
				var tableRow = GetTableRow(row, 0);
				Table.AddView(tableRow);
			}
		}

		View GetHeader(int section)
		{
			string label = Dashboard.SectionTitles[section];
			if (sharedController.RequestList != null) {
				label += " (" + sharedController.RequestList.Length + ")";
			}
			var header = this.GetSectionHeader(label);
			header.SetBackgroundColor(Dashboard.SectionBackgroundColors[section].GetAndroidColor());
			return header;
		}

		TableRow GetTableRow(int row, int section)
		{
			var rowView = this.GetRequestRow(sharedController.RequestList[row]);
			Android.Graphics.Color color = Dashboard.SectionBackgroundColors[section].GetAndroidColor();
			color.A = Dashboard.RowAlphaByte;
			rowView.SetBackgroundColor(color);
			this.HideRequestStatusLabel(rowView);
			return rowView;
		}
	}
}
