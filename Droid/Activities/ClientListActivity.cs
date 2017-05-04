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
	/// Client list activity.
	/// </summary>
	[Activity(Label = "ClientListActivity")]
	public class ClientListActivity : AppCompatActivity
	{
		readonly ClientListManager Manager = new ClientListManager();

		TableLayout Table { get { return FindViewById<TableLayout>(Resource.Id.tableLayout); } }
		LinearLayout Layout { get { return FindViewById<LinearLayout>(Resource.Id.mainLayout); } }

		HorizontalScrollView _HeaderScrollView;
		HorizontalScrollView HeaderScrollView {
			get {
				if (_HeaderScrollView != null) return _HeaderScrollView;
				_HeaderScrollView = this.GetTopButtons(ClientListManager.ButtonLabels);
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

		bool CurrentListType {
			set {
				Manager.AuthorizedListType = value;
				GetClients();
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
			GetClients();
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			this.CreateMainOptionsMenu(menu, Resource.Id.ClientIcon);
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
			for (int row = 0; row < Manager.Clients.Length; row++) {
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
			CurrentListType = GetClientListType(sender);
			SetButtons(sender as Button);
		}

		void SetButtons(Button activeButton)
		{
			foreach (var button in TopButtons) {
				button.SetTextColor(ClientListManager.InactiveTextColor.GetAndroidColor());
				button.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(ClientListManager.GetListTypeColor(GetClientListType(button)).GetAndroidColor());
				button.Enabled = true;
			}
			activeButton.SetTextColor(Manager.CurrentListTypeColor.GetAndroidColor());
			activeButton.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(ClientListManager.ActiveBackgroundColor.GetAndroidColor());
			activeButton.Enabled = false;
		}

		bool GetClientListType(object sender)
		{
			return sender == TopButtons[0];
		}

		View GetTableHeader()
		{
			string label = Manager.CurrentListTypeTitle;
			if (Manager.Clients != null) {
				label += " (" + Manager.Clients.Length + ")";
			}
			var header = this.GetTableSectionHeader(label);
			header.SetBackgroundColor(Manager.CurrentListTypeColor.GetAndroidColor());
			return header;
		}

		TableRow GetTableRow(int row)
		{
			var rowView = this.GetTableRow(Manager.Clients[row]);
			rowView.SetBackgroundColor(Manager.CurrentListTypeColor.ColorWithAlpha(ClientListManager.TableRowAlphaByte));
			return rowView;
		}

		public void GetClients()
		{
			Manager.GetClients(
				delegate { RunOnUiThread(delegate { InitializeTable(); }); },
				(Exception e) => { RunOnUiThread(delegate { this.ShowPrompt(Localizable.PromptMessages.ClientsError); }); }
			);
		}
	}
}
