using System;
using System.Net;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Support.V7.App;

namespace Commercially.Droid
{
	[Activity(Label = "LoginActivity", NoHistory = true)]
	public class LoginActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Login);
			SupportActionBar.Hide();

			var EmailField = FindViewById<EditText>(Resource.Id.EmailField);
			var PasswordField = FindViewById<EditText>(Resource.Id.PasswordField);
			var LoginButton = FindViewById<Button>(Resource.Id.LoginButton);

			LoginButton.Click += delegate {
				if (string.IsNullOrWhiteSpace(EmailField.Text) || string.IsNullOrWhiteSpace(PasswordField.Text)) return;
				try {
					var response = UserApi.LoginUser(EmailField.Text, PasswordField.Text);
					Session.OAuth = new OAuthResponse(response);
					Session.User = new User(EmailField.Text, PasswordField.Text);
					StartActivity(new Intent(this, typeof(DashboardActivity)));
				} catch (Exception) {
					var newFragment = new PromptDialogFragment(Localizable.PromptMessages.LoginError);
					newFragment.Show(FragmentManager, "Error");
				}
			};
		}
	}
}

