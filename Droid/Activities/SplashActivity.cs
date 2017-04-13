using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;

namespace Commercially.Droid
{
	[Activity(Label = "Commercially", MainLauncher = true, NoHistory = true)]
	public class SplashActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			Main.Initialize();
            SetContentView(Resource.Layout.Splash);
			SupportActionBar.Hide();
		}

		protected override void OnResume()
		{
			base.OnResume();
			Session.TaskFactory.StartNew(delegate {
				Thread.Sleep(2000);
				StartActivity(new Intent(this, typeof(LoginActivity)));
			});
		}
	}
}