using System;
using Android.App;
using Android.Content;
using Android.Views;

namespace Commercially.Droid
{
	public static class ActivityExtensions
	{
		public static void StartActivityMenuItem(this Activity activity, IMenuItem item)
		{
			switch (item.ItemId) {
				case Resource.Id.DashboardIcon:
					activity.StartActivity(new Intent(activity, typeof(DashboardActivity)));
					break;
				case Resource.Id.ListIcon:
					activity.StartActivity(new Intent(activity, typeof(RequestListActivity)));
					break;
				case Resource.Id.ButtonIcon:
					activity.StartActivity(new Intent(activity, typeof(ButtonListActivity)));
					break;
			}
		}

		// Call this before SetContentView
		public static void ForceHideActionBar(this Activity activity)
		{
			activity.Window.RequestFeature(WindowFeatures.ActionBar);
			activity.ActionBar.Hide();
		}

		public static void ShowPrompt(this Activity activity, string message) {
			var newFragment = new PromptDialogFragment(message);
			newFragment.Show(activity.FragmentManager, message);
		}
	}
}
