// Created by Philip Gai

using Android.Support.V7.App;

namespace Commercially.Droid
{
	/// <summary>
	/// App compat activity extensions.
	/// </summary>
	public static class AppCompatActivityExtensions
	{
		/// <summary>
		/// Shows the back arrow.
		/// </summary>
		/// <param name="activity">Activity.</param>
		public static void ShowBackArrow(this AppCompatActivity activity)
		{
			activity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			activity.SupportActionBar.SetDisplayShowHomeEnabled(true);
			activity.SupportActionBar.SetDisplayShowTitleEnabled(false);
		}

		/// <summary>
		/// Sets the support action bar default values.
		/// </summary>
		/// <param name="activity">Activity.</param>
		/// <param name="title">Title.</param>
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
