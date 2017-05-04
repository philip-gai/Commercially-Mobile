// Created by Philip Gai

using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Commercially.Droid
{
	/// <summary>
	/// Request list activity.
	/// </summary>
	[Activity(Label = "RequestListActivity")]
	public class RequestListActivity : AppCompatActivity
	{
		readonly RequestListManager Manager = new RequestListManager();

		TableLayout Table { get { return FindViewById<TableLayout>(Resource.Id.tableLayout); } }
		LinearLayout Layout { get { return FindViewById<LinearLayout>(Resource.Id.mainLayout); } }

		HorizontalScrollView _HeaderScrollView;
		HorizontalScrollView HeaderScrollView {
			get {
				if (_HeaderScrollView != null) return _HeaderScrollView;
				_HeaderScrollView = this.GetTopButtons(RequestListManager.RequestTypes);
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

		RequestStatusType CurrentListType {
			set {
				Manager.CurrentListType = value;
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
			InvalidateOptionsMenu();
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
			var header = GetTableHeader();
			Table.AddView(header);
			for (int row = 0; row < Manager.Requests.Length; row++) {
				var tableRow = GetTableRow(row);
				Table.AddViewWithUnderline(tableRow, this);
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
			CurrentListType = GetRequestListType(sender);
			SetButtons(sender as Button);
		}

		void SetButtons(Button activeButton)
		{
			foreach (var button in TopButtons) {
				button.SetTextColor(RequestListManager.InactiveTextColor.GetAndroidColor());
				button.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(RequestListManager.GetListTypeColor(GetRequestListType(button)).GetAndroidColor());
				button.Enabled = true;
			}
			activeButton.SetTextColor(Manager.CurrentListTypeColor.GetAndroidColor());
			activeButton.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(RequestListManager.ActiveBackgroundColor.GetAndroidColor());
			activeButton.Enabled = false;
		}

		// Gets request list type based on the button text
		RequestStatusType GetRequestListType(object sender)
		{
			var button = sender as Button;
			foreach (var type in RequestListManager.RequestTypes) {
				if (type.ToString().Equals(button.Text, StringComparison.CurrentCultureIgnoreCase)) {
					return type;
				}
			}
			return RequestStatusType.New;
		}

		View GetTableHeader()
		{
			string label = Manager.CurrentListTypeTitle;
			if (Manager.Requests != null) {
				label += " (" + Manager.Requests.Length + ")";
			}
			var header = this.GetTableSectionHeader(label);
			header.SetBackgroundColor(Manager.CurrentListTypeColor.GetAndroidColor());
			return header;
		}

		TableRow GetTableRow(int row)
		{
			var rowView = this.GetTableRow(Manager.Requests[row]);
			rowView.SetBackgroundColor(Manager.CurrentListTypeColor.ColorWithAlpha(RequestListManager.TableRowAlphaByte));
			return rowView;
		}

		public void GetRequests()
		{
			Manager.GetRequests(
				delegate { RunOnUiThread(delegate { InitializeTable(); }); },
				(Exception e) => { RunOnUiThread(delegate { this.ShowPrompt(Localizable.PromptMessages.RequestsError); }); }
			);
		}
	}
}
