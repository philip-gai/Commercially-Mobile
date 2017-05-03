
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Commercially.Droid
{
	[Activity(Label = "UserDetailsActivity")]
	public class UserDetailsActivity : AppCompatActivity
	{
		readonly UserDetails SharedController = new UserDetails();

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
				SharedController.User = user;
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
				SharedController.User = Session.User;
			}
            InitializeView();
		}

		protected override void OnPause()
		{
			base.OnPause();
			RemoveListeners();
		}

		void RemoveListeners() {
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
			if (SharedController.User == null) return;

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
			NameField.Text = SharedController.NameText;
			UsernameField.Text = SharedController.UsernameText;
			EmailField.Text = SharedController.EmailText;
			PhoneField.Text = SharedController.PhoneText;

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
			ChangePasswordButton.Hidden(SharedController.ChangePasswordButtonIsHidden);
		}

		void InitializeTable()
		{
			Table.RemoveAllViews();
			var header = GetHeader();
			Table.AddView(header);
			for (int row = 0; row < SharedController.Requests.Length; row++) {
				var tableRow = GetTableRow(row);
				Table.AddViewWithUnderline(tableRow, this);
			}
		}

		void FieldTextChanged(object sender, TextChangedEventArgs e)
		{
			SaveButton.Hidden(!SharedController.FieldsChanged(NameField.Text, UsernameField.Text, EmailField.Text,
															  PhoneField.Text, NewPasswordField.Text,
															  RepeatNewPasswordField.Text));
		}

		void SaveButtonClick(object sender, EventArgs e)
		{
			try {
				SharedController.SaveButtonPress(NameField.Text, UsernameField.Text, EmailField.Text,
												 PhoneField.Text, OldPasswordField.Text,
												 NewPasswordField.Text, RepeatNewPasswordField.Text);
			} catch (Exception) {
				if (UserDetails.PasswordsChanged(NewPasswordField.Text, RepeatNewPasswordField.Text)) {
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
				SharedController.User = Session.User;
			}
		}

		void ChangePasswordButtonClick(object sender, EventArgs e)
		{
			OldPasswordField.Hidden(OldPasswordField.Visibility == ViewStates.Visible);
			NewPasswordField.Hidden(NewPasswordField.Visibility == ViewStates.Visible);
			RepeatNewPasswordField.Hidden(RepeatNewPasswordField.Visibility == ViewStates.Visible);
		}

		View GetHeader()
		{
			string label = UserDetails.HeaderTitle;
			if (SharedController.Requests != null) {
				label += " (" + SharedController.Requests.Length + ")";
			}
			var header = this.GetSectionHeader(label);
			header.SetBackgroundColor(UserDetails.TableHeaderColor.GetAndroidColor());
			return header;
		}

		TableRow GetTableRow(int row)
		{
			var rowView = this.GetTableRow(SharedController.Requests[row]);
			rowView.SetBackgroundColor(UserDetails.TableHeaderColor.ColorWithAlpha(RequestList.RowAlphaByte));
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
