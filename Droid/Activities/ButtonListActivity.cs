using System;
using System.Collections.Generic;
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
		readonly ButtonList SharedController = new ButtonList();

		TableLayout Table { get { return FindViewById<TableLayout>(Resource.Id.tableLayout); } }
		LinearLayout Layout { get { return FindViewById<LinearLayout>(Resource.Id.mainLayout); } }
		LinearLayout _ButtonLayout;
		LinearLayout ButtonLayout {
			get {
				if (_ButtonLayout != null) return _ButtonLayout;
				_ButtonLayout = this.GetButtonListHeader();
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

		ButtonType CurrentType {
			set {
				SharedController.CurrentType = value;
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
			var header = GetHeader();
			Table.AddView(header);
			for (int row = 0; row < SharedController.Buttons.Length; row++) {
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
			CurrentType = GetButtonType(sender);
			var senderButton = sender as Button;
			foreach (var button in TopButtons) {
				button.SetTextColor(ButtonList.InactiveColor.GetAndroidColor());
				button.Enabled = true;
			}
			senderButton.SetTextColor(ButtonList.ActiveColor.GetAndroidColor());
			senderButton.Enabled = false;
		}

		ButtonType GetButtonType(object sender)
		{
			var button = sender as Button;
			foreach (var type in ButtonList.ButtonTypes) {
				if (type.ToString().Equals(button.Text)) {
					return type;
				}
			}
			return ButtonType.Paired;
		}

		View GetHeader()
		{
			string label = SharedController.SectionTitle;
			if (SharedController.Buttons != null) {
				label += " (" + SharedController.Buttons.Length + ")";
			}
			var header = this.GetSectionHeader(label);
			header.SetBackgroundColor(ButtonList.TableBackgroundColor.GetAndroidColor());
			return header;
		}

		TableRow GetTableRow(int row)
		{
			var rowView = this.GetButtonRow(SharedController.Buttons[row]);
			Android.Graphics.Color color = ButtonList.TableBackgroundColor.GetAndroidColor();
			color.A = ButtonList.RowAlphaByte;
			rowView.SetBackgroundColor(color);
			return rowView;
		}

		public void GetButtons()
		{
			SharedController.GetButtons(
				delegate { RunOnUiThread(delegate { InitializeTable(); }); },
				(Exception e) => { RunOnUiThread(delegate { this.ShowPrompt(Localizable.PromptMessages.ButtonsError); }); }
			);
		}
	}
}