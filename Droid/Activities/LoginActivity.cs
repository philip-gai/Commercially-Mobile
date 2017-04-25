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
		EditText EmailField { get { return FindViewById<EditText>(Resource.Id.EmailField); } }
		EditText PasswordField { get { return FindViewById<EditText>(Resource.Id.PasswordField); } }
		Button LoginButton { get { return FindViewById<Button>(Resource.Id.LoginButton); } }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Login);
			SupportActionBar?.Hide();
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

			LoginButton.Click += LoginButtonClick;
		}

		void LoginButtonClick(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(EmailField.Text) || string.IsNullOrWhiteSpace(PasswordField.Text)) return;
			try {
				var response = UserApi.LoginUser(EmailField.Text, PasswordField.Text);
				Session.OAuth = new OAuthResponse(response);
				Session.User = UserApi.GetCurrentUser();
				StartActivity(new Intent(this, typeof(DashboardActivity)));
			} catch (Exception) {
				this.ShowPrompt(Localizable.PromptMessages.LoginError);
			}
		}
	}
}

