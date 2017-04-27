using System;
using System.Collections.Generic;
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
		readonly RequestList SharedController = new RequestList();

		TableLayout Table { get { return FindViewById<TableLayout>(Resource.Id.tableLayout); } }
		LinearLayout Layout { get { return FindViewById<LinearLayout>(Resource.Id.mainLayout); } }

		HorizontalScrollView _HeaderScrollView;
		HorizontalScrollView HeaderScrollView {
			get {
				if (_HeaderScrollView != null) return _HeaderScrollView;
				_HeaderScrollView = this.GetTopButtons(RequestList.RequestTypes);
				return _HeaderScrollView;
			}
		}
		LinearLayout ButtonLayout {
			get {
				return HeaderScrollView.FindViewById<LinearLayout>(Resource.Id.buttonLayout);
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
			Layout.AddView(HeaderScrollView, 0);
			foreach (var button in TopButtons) {
				button.Click += TopButtonClick;
			}
			SetButtons(TopButtons[0]);
		}

		void TopButtonClick(object sender, EventArgs e)
		{
			CurrentType = GetRequestStatusType(sender);
			SetButtons(sender as Button);
		}

		void SetButtons(Button activeButton)
		{
			foreach (var button in TopButtons) {
				button.SetTextColor(RequestList.InactiveTextColor.GetAndroidColor());
				button.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(RequestList.GetTypeColor(GetRequestStatusType(button)).GetAndroidColor());
				button.Enabled = true;
			}
			activeButton.SetTextColor(SharedController.CurrentTypeColor.GetAndroidColor());
			activeButton.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(RequestList.ActiveBackgroundColor.GetAndroidColor());
			activeButton.Enabled = false;
		}

		RequestStatusType GetRequestStatusType(object sender)
		{
			var button = sender as Button;
			foreach (var type in RequestList.RequestTypes) {
				if (type.ToString().Equals(button.Text, StringComparison.CurrentCultureIgnoreCase)) {
					return type;
				}
			}
			return RequestStatusType.New;
		}

		View GetHeader()
		{
			string label = SharedController.CurrentTypeTitle;
			if (SharedController.Requests != null) {
				label += " (" + SharedController.Requests.Length + ")";
			}
			var header = this.GetSectionHeader(label);
			header.SetBackgroundColor(SharedController.CurrentTypeColor.GetAndroidColor());
			return header;
		}

		TableRow GetTableRow(int row)
		{
			var rowView = this.GetTableRow(SharedController.Requests[row]);
			rowView.SetBackgroundColor(SharedController.CurrentTypeColor.ColorWithAlpha(RequestList.RowAlphaByte));
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
