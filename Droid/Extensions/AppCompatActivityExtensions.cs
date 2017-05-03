using Android.Support.V7.App;

namespace Commercially.Droid
{
	public static class AppCompatActivityExtensions
	{
		public static void ShowBackArrow(this AppCompatActivity activity)
		{
			activity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			activity.SupportActionBar.SetDisplayShowHomeEnabled(true);
			activity.SupportActionBar.SetDisplayShowTitleEnabled(false);
		}

		public static void SetSupportActionBarDefault(this AppCompatActivity activity, string title = "")
		{
			activity.SupportActionBar.Show();
			activity.SupportActionBar.Title = "  " + title;
			activity.SupportActionBar.SetDisplayShowHomeEnabled(true);
			activity.SupportActionBar.SetIcon(Resource.Drawable.LogoRed);
			activity.SupportActionBar.SetDisplayShowTitleEnabled(true);
		}
	}
}
