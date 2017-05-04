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
	/// Button list activity.
	/// </summary>
	[Activity(Label = "ButtonListActivity")]
	public class ButtonListActivity : AppCompatActivity
	{
		readonly ButtonListManager Manager = new ButtonListManager();

		TableLayout Table { get { return FindViewById<TableLayout>(Resource.Id.tableLayout); } }
		LinearLayout Layout { get { return FindViewById<LinearLayout>(Resource.Id.mainLayout); } }

		// The scroll view for the top button bar
		HorizontalScrollView _HeaderScrollView;
		HorizontalScrollView HeaderScrollView {
			get {
				if (_HeaderScrollView != null) return _HeaderScrollView;
				_HeaderScrollView = this.GetTopButtons(ButtonListManager.ButtonListTypes);
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

		ButtonType CurrentListType {
			set {
				Manager.CurrentListType = value;
				GetButtons();
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
			GetButtons();
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			this.CreateMainOptionsMenu(menu, Resource.Id.ButtonIcon);
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
			for (int row = 0; row < Manager.Buttons.Length; row++) {
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
			CurrentListType = GetButtonListType(sender);
			SetButtons(sender as Button);
		}

		void SetButtons(Button activeButton)
		{
			foreach (var button in TopButtons) {
				button.SetTextColor(ButtonListManager.InactiveTextColor.GetAndroidColor());
				button.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(ButtonListManager.GetListTypeColor(GetButtonListType(button)).GetAndroidColor());
				button.Enabled = true;
			}
			activeButton.SetTextColor(Manager.CurrentListTypeColor.GetAndroidColor());
			activeButton.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(ButtonListManager.ActiveBackgroundColor.GetAndroidColor());
			activeButton.Enabled = false;
		}

		// Gets the type of the button list based on the button that was clicked
		ButtonType GetButtonListType(object sender)
		{
			var button = sender as Button;
			foreach (var type in ButtonListManager.ButtonListTypes) {
				if (type.ToString().Equals(button.Text, StringComparison.CurrentCultureIgnoreCase)) {
					return type;
				}
			}
			return ButtonType.Paired;
		}

		View GetTableHeader()
		{
			string label = Manager.CurrentListTypeTitle;
			if (Manager.Buttons != null) {
				label += " (" + Manager.Buttons.Length + ")";
			}
			var header = this.GetTableSectionHeader(label);
			header.SetBackgroundColor(Manager.CurrentListTypeColor.GetAndroidColor());
			return header;
		}

		TableRow GetTableRow(int row)
		{
			var rowView = this.GetTableRow(Manager.Buttons[row]);
			rowView.SetBackgroundColor(Manager.CurrentListTypeColor.ColorWithAlpha(ButtonListManager.TableRowAlphaByte));
			return rowView;
		}

		public void GetButtons()
		{
			Manager.GetButtons(
				delegate { RunOnUiThread(delegate { InitializeTable(); }); },
				(Exception e) => { RunOnUiThread(delegate { this.ShowPrompt(Localizable.PromptMessages.ButtonsError); }); }
			);
		}
	}
}