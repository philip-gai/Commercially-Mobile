using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Support.V7.App;
using Android.Views;

namespace Commercially.Droid
{
	[Activity(Label = "LoginActivity", NoHistory = true)]
	public class LoginActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Login);
			SupportActionBar?.Hide();
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

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
					this.ShowPrompt(Localizable.PromptMessages.LoginError);
				}
			};
		}
	}
}

