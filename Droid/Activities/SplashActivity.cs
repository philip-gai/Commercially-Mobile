using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;

namespace Commercially.Droid
{
	[Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true, Icon = "@mipmap/icon")]
	public class SplashActivity : AppCompatActivity
	{
		static readonly string TAG = "X:" + typeof(SplashActivity).Name;

		public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
		{
			base.OnCreate(savedInstanceState, persistentState);
			Log.Debug(TAG, "SplashActivity.OnCreate");
		}

		// Launches the startup task
		protected override void OnResume()
		{
			base.OnResume();
			Session.TaskFactory.StartNew(SimulateStartup);
		}

		// Simulates background work that happens behind the splash screen
		void SimulateStartup()
		{
			Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
			Task.Delay(5000);
			Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
			StartActivity(new Intent(Application.Context, typeof(LoginActivity)));
		}
	}
}