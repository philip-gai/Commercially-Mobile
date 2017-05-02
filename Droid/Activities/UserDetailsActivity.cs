
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
			this.ShowBackArrow();

			var user = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra(typeof(User).Name));
			SharedController.User = user;
			InitializeView();
		}

		protected override void OnResume()
		{
			base.OnResume();
			GetRequests();
		}

		public override bool OnSupportNavigateUp()
		{
			Finish();
			return true;
		}

		void InitializeView()
		{
			if (SharedController.User == null) return;
			NameField.Text = SharedController.NameText;
			EmailField.Text = SharedController.EmailText;
			PhoneField.Text = SharedController.PhoneText;

			SaveButton.Click += SaveButtonClick;
			ChangePasswordButton.Click += ChangePasswordButtonClick;
			PhoneField.Hidden(SharedController.PhoneFieldIsHidden);
			SaveButton.Hidden(true);
			OldPasswordField.Hidden(true);
			NewPasswordField.Hidden(true);
			RepeatNewPasswordField.Hidden(true);

			if (!SharedController.IsEditable) {
				NameField.Enabled = false;
				EmailField.Enabled = false;
				PhoneField.Enabled = false;
				ChangePasswordButton.Hidden(true);
			} else {
				NameField.TextChanged += FieldTextChanged;
				EmailField.TextChanged += FieldTextChanged;
				PhoneField.TextChanged += FieldTextChanged;
				NewPasswordField.TextChanged += FieldTextChanged;
				RepeatNewPasswordField.TextChanged += FieldTextChanged;
			}

			Layout.AddView(Table);
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
			SaveButton.Hidden(!SharedController.FieldsChanged(NameField.Text, EmailField.Text,
															  PhoneField.Text, NewPasswordField.Text,
															  RepeatNewPasswordField.Text));
		}

		void SaveButtonClick(object sender, EventArgs e)
		{
			try {
				SharedController.SaveButtonPress(NameField.Text, EmailField.Text,
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
			Finish();
		}

		void ChangePasswordButtonClick(object sender, EventArgs e)
		{
			OldPasswordField.Hidden(OldPasswordField.Visibility == ViewStates.Visible);
			NewPasswordField.Hidden(OldPasswordField.Visibility == ViewStates.Visible);
			RepeatNewPasswordField.Hidden(OldPasswordField.Visibility == ViewStates.Visible);
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
