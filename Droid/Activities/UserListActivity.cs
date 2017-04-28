using System;
using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Commercially.Droid
{
	[Activity(Label = "UserListActivity")]
	public class UserListActivity : AppCompatActivity
	{
		readonly UserList SharedController = new UserList();

		TableLayout Table { get { return FindViewById<TableLayout>(Resource.Id.tableLayout); } }
		LinearLayout Layout { get { return FindViewById<LinearLayout>(Resource.Id.mainLayout); } }

		HorizontalScrollView _HeaderScrollView;
		HorizontalScrollView HeaderScrollView {
			get {
				if (_HeaderScrollView != null) return _HeaderScrollView;
				_HeaderScrollView = this.GetTopButtons(UserList.UserRoleTypes);
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

		UserRoleType CurrentType {
			set {
				SharedController.CurrentType = value;
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
			var header = GetHeader();
			Table.AddView(header);
			for (int row = 0; row < SharedController.Users.Length; row++) {
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
			CurrentType = GetUserRoleType(sender);
			SetButtons(sender as Button);
		}

		void SetButtons(Button activeButton)
		{
			foreach (var button in TopButtons) {
				button.SetTextColor(UserList.InactiveTextColor.GetAndroidColor());
				button.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(UserList.GetTypeColor(GetUserRoleType(button)).GetAndroidColor());
				button.Enabled = true;
			}
			activeButton.SetTextColor(SharedController.CurrentTypeColor.GetAndroidColor());
			activeButton.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(UserList.ActiveBackgroundColor.GetAndroidColor());
			activeButton.Enabled = false;
		}

		UserRoleType GetUserRoleType(object sender)
		{
			var button = sender as Button;
			foreach (var type in UserList.UserRoleTypes) {
				if (type.ToString().Equals(button.Text.Substring(0, button.Text.Length - 1), StringComparison.CurrentCultureIgnoreCase)) {
					return type;
				}
			}
			return UserRoleType.Worker;
		}

		View GetHeader()
		{
			string label = SharedController.CurrentTypeTitle;
			if (SharedController.Users != null) {
				label += " (" + SharedController.Users.Length + ")";
			}
			var header = this.GetSectionHeader(label);
			header.SetBackgroundColor(SharedController.CurrentTypeColor.GetAndroidColor());
			return header;
		}

		TableRow GetTableRow(int row)
		{
			var rowView = this.GetTableRow(SharedController.Users[row]);
			rowView.SetBackgroundColor(SharedController.CurrentTypeColor.ColorWithAlpha(UserList.RowAlphaByte));
			return rowView;
		}

		public void GetUsers()
		{
			SharedController.GetUsers(
				delegate { RunOnUiThread(delegate { InitializeTable(); }); },
				(Exception e) => { RunOnUiThread(delegate { this.ShowPrompt(Localizable.PromptMessages.UsersError); }); }
			);
		}
	}
}