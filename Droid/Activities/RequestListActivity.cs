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

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Table);
            this.SetSupportActionBarDefault();
			sharedController.GetRequests(
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
			menu.RemoveItem(Resource.Id.ListIcon);
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
			var header = GetHeader();
			table.AddView(header);
			for (int row = 0; row < sharedController.NewRequestList.Length; row++) {
				var tableRow = GetTableRow(row);
				table.AddView(tableRow);
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
			return rowView;
		}
	}
}
