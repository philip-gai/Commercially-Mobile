using System;

using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Commercially.Droid
{
	[Activity(Label = "ButtonListActivity")]
	public class ButtonListActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Table);
			SupportActionBar.Title = "Buttons";
			ButtonList.GetButtons(
				delegate {
					RunOnUiThread(delegate {
						InitializeTable();
					});
				},
				(Exception obj) => {
					RunOnUiThread(delegate {
						this.ShowPrompt(Localizable.PromptMessages.ButtonsError);
					});
				}
			);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.TopMenu, menu);
			menu.RemoveItem(Resource.Id.ButtonIcon);
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
			for (int row = 0; row < Session.Buttons.Length; row++) {
				var tableRow = GetTableRow(row);
				table.AddView(tableRow);
			}
		}

		View GetHeader()
		{
			var inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
			var headerView = (TableRow)inflater.Inflate(Resource.Layout.RequestHeader, null);
			headerView.SetBackgroundColor(ButtonList.TableBackgroundColor.GetAndroidColor());
			var headerLabel = headerView.FindViewById<TextView>(Resource.Id.headerText);
			headerLabel.Text = RequestStatusType.New.ToString();
			if (Session.Buttons != null) {
				headerLabel.Text += " (" + Session.Buttons.Length + ")";
			}
			return headerView;
		}

		TableRow GetTableRow(int row)
		{
			var inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
			var rowView = (TableRow)inflater.Inflate(Resource.Layout.RequestRow, null);
			//Android.Graphics.Color color = ButtonList.TableBackgroundColor.GetAndroidColor();
			//color.A = ButtonList.RowAlphaByte;
			//rowView.SetBackgroundColor(color);
			//var description = rowView.FindViewById<TextView>(Resource.Id.descriptionText);
			//var locationLabel = rowView.FindViewById<TextView>(Resource.Id.locationText);
			//var timeLabel = rowView.FindViewById<TextView>(Resource.Id.timeText);
			//var statusLabel = rowView.FindViewById<TextView>(Resource.Id.statusText);
			//var urgentIndicator = rowView.FindViewById(Resource.Id.urgentIndicator);

			//var request = Session.Buttons[row];
			//description.Text = request.description;
			//locationLabel.Text = request.room;
			//timeLabel.Text = request.GetTime(Request.TimeType.Received)?.ToShortTimeString();
			//statusLabel.Text = request.GetStatus().ToString();
			//urgentIndicator.Visibility = request.urgent ? ViewStates.Visible : ViewStates.Gone;
			return rowView;
		}
	}
}
