using Android.App;
using Android.Widget;
using Android.OS;

namespace Commercially.Droid {
	[Activity(Label = "Commercially", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity {
		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Login);

			// Get our button from the layout resource,
			// and attach an event to it
			EditText email = FindViewById<EditText>(Resource.Id.EmailField);
			EditText password = FindViewById<EditText>(Resource.Id.PasswordField);
			Button button = FindViewById<Button>(Resource.Id.LoginButton);
			button.Click += delegate {
				if (Validator.Email(email.Text) && Validator.Password(password.Text)) {
					button.Text = "Email and Password were valid!";
				} else {
					if (!Validator.Email(email.Text)) {
						button.Text = "Email is invalid!";
					} else if (Validator.Password(password.Text)) { 
						button.Text = "Password is invalid!";
					}
				}
			};
		}
	}
}

