// Created by Philip Gai

using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Commercially.Droid
{
	/// <summary>
	/// User details activity.
	/// </summary>
	[Activity(Label = "UserDetailsActivity")]
	public class UserDetailsActivity : AppCompatActivity
	{
		readonly UserDetailsManager Manager = new UserDetailsManager();

		LinearLayout Layout { get { return FindViewById<LinearLayout>(Resource.Id.mainLayout); } }
		EditText NameField { get { return FindViewById<EditText>(Resource.Id.nameField); } }
		EditText UsernameField { get { return FindViewById<EditText>(Resource.Id.usernameField); } }
		EditText EmailField { get { return FindViewById<EditText>(Resource.Id.emailField); } }
		EditText PhoneField { get { return FindViewById<EditText>(Resource.Id.phoneField); } }
		EditText OldPasswordField { get { return FindViewById<EditText>(Resource.Id.oldPasswordField); } }
		EditText NewPasswordField { get { return FindViewById<EditText>(Resource.Id.newPasswordField); } }
		EditText RepeatNewPasswordField { get { return FindViewById<EditText>(Resource.Id.repeatNewPasswordField); } }
		Button ChangePasswordButton { get { return FindViewById<Button>(Resource.Id.changePasswordButton); } }
		Button SaveButton { get { return FindViewById<Button>(Resource.Id.saveButton); } }

		LinearLayout _Table;
		LinearLayout Table {
			get {
				if (_Table != null) return _Table;
				var inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
				var table = (LinearLayout)inflater.Inflate(Resource.Layout.Table, null);
				_Table = table;
				return _Table;
			}
		}


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.UserDetails);
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
			if (Session.User.Type == UserRoleType.Admin) {
				this.ShowBackArrow();
				var user = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra(typeof(User).Name));
				Manager.User = user;
			} else {
				this.SetSupportActionBarDefault();
			}
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			if (Session.User.Type != UserRoleType.Admin) {
				this.CreateMainOptionsMenu(menu, Resource.Id.UserIcon);
			}
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			this.StartActivityMenuItem(item);
			return base.OnOptionsItemSelected(item);
		}

		protected override void OnResume()
		{
			base.OnResume();
			InvalidateOptionsMenu();
			if (Session.User.Type == UserRoleType.Admin) {
				GetRequests();
			} else {
				Session.User = UserApi.GetCurrentUser();
				Manager.User = Session.User;
			}
			InitializeView();
		}

		protected override void OnPause()
		{
			base.OnPause();
			RemoveListeners();
		}

		void RemoveListeners()
		{
			SaveButton.Click -= SaveButtonClick;
			ChangePasswordButton.Click -= ChangePasswordButtonClick;
			NameField.TextChanged -= FieldTextChanged;
			UsernameField.TextChanged -= FieldTextChanged;
			EmailField.TextChanged -= FieldTextChanged;
			PhoneField.TextChanged -= FieldTextChanged;
			NewPasswordField.TextChanged -= FieldTextChanged;
			RepeatNewPasswordField.TextChanged -= FieldTextChanged;
		}

		public override bool OnSupportNavigateUp()
		{
			if (Session.User.Type == UserRoleType.Admin) {
				Finish();
				return true;
			}
			return base.OnSupportNavigateUp();
		}

		void InitializeView()
		{
			if (Manager.User == null) return;

			InitializeFields();
			InitializeVisibility();

			if (Session.User.Type == UserRoleType.Admin) {
				Layout.AddView(Table);
			}
			SaveButton.Click += SaveButtonClick;
			ChangePasswordButton.Click += ChangePasswordButtonClick;
		}

		void InitializeFields()
		{
			NameField.Text = Manager.NameText;
			UsernameField.Text = Manager.UsernameText;
			EmailField.Text = Manager.EmailText;
			PhoneField.Text = Manager.PhoneText;

			NameField.TextChanged += FieldTextChanged;
			UsernameField.TextChanged += FieldTextChanged;
			EmailField.TextChanged += FieldTextChanged;
			PhoneField.TextChanged += FieldTextChanged;
			NewPasswordField.TextChanged += FieldTextChanged;
			RepeatNewPasswordField.TextChanged += FieldTextChanged;
		}

		void InitializeVisibility()
		{
			SaveButton.Hidden(true);
			OldPasswordField.Hidden(true);
			NewPasswordField.Hidden(true);
			RepeatNewPasswordField.Hidden(true);
			ChangePasswordButton.Hidden(Manager.ChangePasswordButtonIsHidden);
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

		void FieldTextChanged(object sender, TextChangedEventArgs e)
		{
			SaveButton.Hidden(!Manager.FieldsAreChanged(NameField.Text, UsernameField.Text, EmailField.Text,
															  PhoneField.Text, NewPasswordField.Text,
															  RepeatNewPasswordField.Text));
		}

		void SaveButtonClick(object sender, EventArgs e)
		{
			try {
				Manager.OnSaveButtonPressHandler(NameField.Text, UsernameField.Text, EmailField.Text,
												 PhoneField.Text, OldPasswordField.Text,
												 NewPasswordField.Text, RepeatNewPasswordField.Text);
			} catch (Exception) {
				if (UserDetailsManager.PasswordsAreChanged(NewPasswordField.Text, RepeatNewPasswordField.Text)) {
					this.ShowPrompt(Localizable.PromptMessages.InvalidPassword);
				} else {
					this.ShowPrompt(Localizable.PromptMessages.ChangesSaveError);
				}
				return;
			}
			SaveButton.Hidden(true);
			OldPasswordField.Hidden(true);
			NewPasswordField.Hidden(true);
			RepeatNewPasswordField.Hidden(true);
			if (Session.User.Type == UserRoleType.Admin) {
				Finish();
			} else {
				this.ShowPrompt(Localizable.PromptMessages.SaveSuccess);
				Session.User = UserApi.GetCurrentUser();
				Manager.User = Session.User;
			}
		}

		void ChangePasswordButtonClick(object sender, EventArgs e)
		{
			OldPasswordField.Hidden(OldPasswordField.Visibility == ViewStates.Visible);
			NewPasswordField.Hidden(NewPasswordField.Visibility == ViewStates.Visible);
			RepeatNewPasswordField.Hidden(RepeatNewPasswordField.Visibility == ViewStates.Visible);
		}

		View GetTableHeader()
		{
			string label = UserDetailsManager.TableHeaderTitle;
			if (Manager.Requests != null) {
				label += " (" + Manager.Requests.Length + ")";
			}
			var header = this.GetTableSectionHeader(label);
			header.SetBackgroundColor(UserDetailsManager.TableHeaderColor.GetAndroidColor());
			return header;
		}

		TableRow GetTableRow(int row)
		{
			var rowView = this.GetTableRow(Manager.Requests[row]);
			rowView.SetBackgroundColor(UserDetailsManager.TableHeaderColor.ColorWithAlpha(RequestListManager.TableRowAlphaByte));
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
