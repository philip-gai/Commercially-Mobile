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
	/// User list activity.
	/// </summary>
	[Activity(Label = "UserListActivity")]
	public class UserListActivity : AppCompatActivity
	{
		readonly UserListManager Manager = new UserListManager();

		TableLayout Table { get { return FindViewById<TableLayout>(Resource.Id.tableLayout); } }
		LinearLayout Layout { get { return FindViewById<LinearLayout>(Resource.Id.mainLayout); } }

		HorizontalScrollView _HeaderScrollView;
		HorizontalScrollView HeaderScrollView {
			get {
				if (_HeaderScrollView != null) return _HeaderScrollView;
				_HeaderScrollView = this.GetTopButtons(UserListManager.UserRoleTypes);
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
						((Button)view).Text += "s";
						list.Add((view as Button));
					}
				}
				_TopButtons = list.ToArray();
				return _TopButtons;
			}
		}

		UserRoleType CurrentListType {
			set {
				Manager.CurrentListType = value;
				GetUsers();
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
			GetUsers();
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			this.CreateMainOptionsMenu(menu, Resource.Id.UserIcon);
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
			for (int row = 0; row < Manager.Users.Length; row++) {
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
			CurrentListType = GetUserListType(sender);
			SetButtons(sender as Button);
		}

		void SetButtons(Button activeButton)
		{
			foreach (var button in TopButtons) {
				button.SetTextColor(UserListManager.InactiveTextColor.GetAndroidColor());
				button.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(UserListManager.GetListTypeColor(GetUserListType(button)).GetAndroidColor());
				button.Enabled = true;
			}
			activeButton.SetTextColor(Manager.CurrentListTypeColor.GetAndroidColor());
			activeButton.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(UserListManager.ActiveBackgroundColor.GetAndroidColor());
			activeButton.Enabled = false;
		}

		// Gets the user list type based on the button that was pressed.
		UserRoleType GetUserListType(object sender)
		{
			var button = sender as Button;
			foreach (var type in UserListManager.UserRoleTypes) {
				if (type.ToString().Equals(button.Text.Substring(0, button.Text.Length - 1), StringComparison.CurrentCultureIgnoreCase)) {
					return type;
				}
			}
			return UserRoleType.Worker;
		}

		View GetTableHeader()
		{
			string label = Manager.CurrentListTypeTitle;
			if (Manager.Users != null) {
				label += " (" + Manager.Users.Length + ")";
			}
			var header = this.GetTableSectionHeader(label);
			header.SetBackgroundColor(Manager.CurrentListTypeColor.GetAndroidColor());
			return header;
		}

		TableRow GetTableRow(int row)
		{
			var rowView = this.GetTableRow(Manager.Users[row]);
			rowView.SetBackgroundColor(Manager.CurrentListTypeColor.ColorWithAlpha(UserListManager.TableRowAlphaByte));
			return rowView;
		}

		public void GetUsers()
		{
			Manager.GetUsers(
				delegate { RunOnUiThread(delegate { InitializeTable(); }); },
				(Exception e) => { RunOnUiThread(delegate { this.ShowPrompt(Localizable.PromptMessages.UsersError); }); }
			);
		}
	}
}