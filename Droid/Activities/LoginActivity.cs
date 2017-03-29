using System;
using System.Net;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Commercially.Droid
{
	[Activity(Label = "Commercially", MainLauncher = true, Icon = "@mipmap/icon")]
	public class LoginActivity : Activity
	{

		protected override void OnCreate(Bundle savedInstanceState)
		{
			ServicePointManager.ServerCertificateValidationCallback = delegate {
				return true;
			};

			base.OnCreate(savedInstanceState);

			// Set our view from the "login" layout resource
			SetContentView(Resource.Layout.Login);

			// Get our button from the layout resource,
			// and attach an event to it
			var EmailField = FindViewById<EditText>(Resource.Id.EmailField);
			var PasswordField = FindViewById<EditText>(Resource.Id.PasswordField);
			var LoginButton = FindViewById<Button>(Resource.Id.LoginButton);

			LoginButton.Click += delegate {
				try {
					//var response = UserApi.LoginUser(EmailField.Text, PasswordField.Text);
					//SessionData.OAuth = new OAuthResponse(response);
					//SessionData.User = new User(EmailField.Text, PasswordField.Text);
					var intent = new Intent(this, typeof(DashboardActivity));
					StartActivity(intent);
				} catch (Exception e) {
					var newFragment = new AlertDialogFragment();
					newFragment.Show(FragmentManager, e.Message);
				}
			};
		}
	}
}

