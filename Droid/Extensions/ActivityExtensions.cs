using System;
using Android.App;
using Android.Content;
using Android.Views;

namespace Commercially.Droid
{
	public static class ActivityExtensions
	{
		public static void StartActivityMenuItem(this Activity activity, IMenuItem item) {
			switch (item.ItemId) {
				//case Resource.Id.icon:
				//	activity.StartActivity(new Intent(activity, typeof(DashboardActivity)));
				//	break;
				//case Resource.Id.ListIcon:
				//	activity.StartActivity(new Intent(activity, typeof(RequestListActivity)));
				//	break;
				//case Resource.Id.ButtonIcon:
				//	break;
			}
		}
	}
}
