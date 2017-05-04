// Created by Philip Gai

using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Commercially.Droid
{
	/// <summary>
	/// User create activity.
	/// </summary>
	[Activity(Label = "UserCreateActivity")]
	public class UserCreateActivity : AppCompatActivity
	{
		EditText NameField { get { return FindViewById<EditText>(Resource.Id.nameField); } }
		EditText UsernameField { get { return FindViewById<EditText>(Resource.Id.usernameField); } }
		EditText EmailField { get { return FindViewById<EditText>(Resource.Id.emailField); } }
		EditText PhoneField { get { return FindViewById<EditText>(Resource.Id.phoneField); } }
		EditText PasswordField { get { return FindViewById<EditText>(Resource.Id.passwordField); } }
		EditText VerifyPasswordField { get { return FindViewById<EditText>(Resource.Id.verifyPasswordField); } }
		Button CreateButton { get { return FindViewById<Button>(Resource.Id.createUserButton); } }
		Spinner UserRoleSpinner;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.UserCreate);
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
			this.ShowBackArrow();

			UserRoleSpinner = this.GetUserRoleSpinner();
			CreateButton.Click += CreateButtonClick;
		}

		public override bool OnSupportNavigateUp()
		{
			Finish();
			return true;
		}

		void CreateButtonClick(object sender, EventArgs e)
		{
			try {
				UserCreateManager.OnCreateButtonPressHandler(NameField.Text, UsernameField.Text,
											 UserRoleSpinner.SelectedItem.ToString(),
											 EmailField.Text, PhoneField.Text, PasswordField.Text, VerifyPasswordField.Text);
			} catch (Exception) {
				this.ShowPrompt(Localizable.PromptMessages.CannotCreateUser);
				return;
			}

			Finish();
		}
	}
}
