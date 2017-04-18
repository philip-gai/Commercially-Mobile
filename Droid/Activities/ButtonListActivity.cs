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
            this.SetSupportActionBarDefault("Buttons");
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

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
			string label = RequestStatusType.New.ToString();
			if (Session.Buttons != null) {
				label += " (" + Session.Buttons.Length + ")";
			}
			var header = this.GetSectionHeader(label);
			header.SetBackgroundColor(ButtonList.TableBackgroundColor.GetAndroidColor());
			return header;
		}

		TableRow GetTableRow(int row)
		{
			var rowView = this.GetButtonRow(Session.Buttons[row]);
			Android.Graphics.Color color = ButtonList.TableBackgroundColor.GetAndroidColor();
			color.A = ButtonList.RowAlphaByte;
			rowView.SetBackgroundColor(color);
			return rowView;
		}
	}
}
