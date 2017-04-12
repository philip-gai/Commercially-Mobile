using System;
using System.Net;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content; 

namespace Commercially.Droid
{
[Activity(Label = "LoginActivity", MainLauncher = true, NoHistory = true)]
	public class LoginActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "login" layout resource
			SetContentView(Resource.Layout.Login);

			Main.Initialize();
			//Window.SetStatusBarColor(GetColor(Resource.Id.);
			ActionBar.Hide();

			// Get our button from the layout resource,
			// and attach an event to it
			var EmailField = FindViewById<EditText>(Resource.Id.EmailField);
			var PasswordField = FindViewById<EditText>(Resource.Id.PasswordField);
			var LoginButton = FindViewById<Button>(Resource.Id.LoginButton);

			LoginButton.Click += delegate {
				try {
					var response = UserApi.LoginUser(EmailField.Text, PasswordField.Text);
					Session.OAuth = new OAuthResponse(response);
					Session.User = new User(EmailField.Text, PasswordField.Text);
					StartActivity(new Intent(this, typeof(DashboardActivity)));
				} catch (Exception e) {
					var newFragment = new PromptDialogFragment(e.Message);
					newFragment.Show(FragmentManager, "Error");
				}
			};
		}
	}
}

