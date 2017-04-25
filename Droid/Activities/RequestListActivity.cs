using System;

using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Commercially.Droid
{
	[Activity(Label = "RequestListActivity")]
	public class RequestListActivity : AppCompatActivity
	{
		readonly RequestList sharedController = new RequestList();

		TableLayout Table { get { return FindViewById<TableLayout>(Resource.Id.tableLayout); } }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Table);
			this.SetSupportActionBarDefault();
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
		}

		protected override void OnResume()
		{
			base.OnResume();
			sharedController.GetRequests(
				delegate { RunOnUiThread(delegate { InitializeTable(); }); },
				(Exception obj) => { RunOnUiThread(delegate { this.ShowPrompt(Localizable.PromptMessages.RequestsError); }); }
			);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			this.CreateMainOptionsMenu(menu, Resource.Id.ListIcon);
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
			var header = GetHeader();
			Table.AddView(header);
			for (int row = 0; row < sharedController.NewRequestList.Length; row++) {
				var tableRow = GetTableRow(row);
				Table.AddView(tableRow);
			}
		}

		View GetHeader()
		{
			string label = RequestStatusType.New.ToString();
			if (sharedController.NewRequestList != null) {
				label += " (" + sharedController.NewRequestList.Length + ")";
			}
			var header = this.GetSectionHeader(label);
			header.SetBackgroundColor(RequestList.TableBackgroundColor.GetAndroidColor());
			return header;
		}

		TableRow GetTableRow(int row)
		{
			var rowView = this.GetRequestRow(sharedController.NewRequestList[row]);
			Android.Graphics.Color color = RequestList.TableBackgroundColor.GetAndroidColor();
			color.A = RequestList.RowAlphaByte;
			rowView.SetBackgroundColor(color);
			this.HideRequestStatusLabel(rowView);
			return rowView;
		}
	}
}
