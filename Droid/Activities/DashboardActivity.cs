using System;
using System.Collections.Generic;
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
		readonly Dashboard SharedController = new Dashboard();

		TableLayout Table { get { return FindViewById<TableLayout>(Resource.Id.tableLayout); } }
		LinearLayout Layout { get { return FindViewById<LinearLayout>(Resource.Id.mainLayout); } }
		LinearLayout _ButtonLayout;
		LinearLayout ButtonLayout {
			get {
				if (_ButtonLayout != null) return _ButtonLayout;
				_ButtonLayout = this.GetDashboardHeader();
				return _ButtonLayout;
			}
		}

		Button[] _TopButtons;
		Button[] TopButtons {
			get {
				if (_TopButtons != null) return _TopButtons;
				var list = new List<Button>();
				for (int i = 0; i < ButtonLayout.ChildCount; i++) {
					var view = ButtonLayout.GetChildAt(i);
					if (view is Button) {
						list.Add((view as Button));
					}
				}
				_TopButtons = list.ToArray();
				return _TopButtons;
			}
		}

		RequestStatusType CurrentType {
			set {
				SharedController.CurrentType = value;
				GetRequests();
			}
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Table);
			this.SetSupportActionBarDefault();
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

			InitializeButtons();

			Home.PrefetchData();
		}

		protected override void OnResume()
		{
			base.OnResume();
			GetRequests();
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
			var header = GetHeader();
			Table.AddView(header);
			for (int row = 0; row < SharedController.Requests.Length; row++) {
				var tableRow = GetTableRow(row);
				Table.AddView(tableRow);
			}
		}

		void InitializeButtons()
		{
			Layout.AddView(ButtonLayout, 0);
			foreach (var button in TopButtons) {
				button.Click += TopButtonClick;
			}
		}

		void TopButtonClick(object sender, EventArgs e)
		{
			CurrentType = GetRequestStatusType(sender);
			var senderButton = sender as Button;
			foreach (var button in TopButtons) {
				button.SetTextColor(Dashboard.InactiveColor.GetAndroidColor());
				button.Enabled = true;
			}
			senderButton.SetTextColor(SharedController.SectionColor.GetAndroidColor());
			senderButton.Enabled = false;
		}

		RequestStatusType GetRequestStatusType(object sender)
		{
			var button = sender as Button;
			foreach (var type in Dashboard.SectionTypes) {
				if (type.ToString().Equals(button.Text)) {
					return type;
				}
			}
			return RequestStatusType.New;
		}

		View GetHeader()
		{
			string label = SharedController.SectionTitle;
			if (SharedController.Requests != null) {
				label += " (" + SharedController.Requests.Length + ")";
			}
			var header = this.GetSectionHeader(label);
			header.SetBackgroundColor(SharedController.SectionColor.GetAndroidColor());
			return header;
		}

		TableRow GetTableRow(int row)
		{
			var rowView = this.GetRequestRow(SharedController.Requests[row]);
			Android.Graphics.Color color = SharedController.SectionColor.GetAndroidColor();
			color.A = Dashboard.RowAlphaByte;
			rowView.SetBackgroundColor(color);
			this.HideRequestStatusLabel(rowView);
			return rowView;
		}

		public void GetRequests()
		{
			SharedController.GetRequests(
				delegate { RunOnUiThread(delegate { InitializeTable(); }); },
				(Exception e) => { RunOnUiThread(delegate { this.ShowPrompt(Localizable.PromptMessages.RequestsError); }); }
			);
		}
	}
}
