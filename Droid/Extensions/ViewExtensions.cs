using System;
using Android.Views;

namespace Commercially.Droid
{
	public static class ViewExtensions
	{
		public static void Hidden(this View view, bool hide)
		{
			view.Visibility = hide ? ViewStates.Gone : ViewStates.Visible;
		}

		public static void ToggleVisibility(this View view)
		{
			switch (view.Visibility) {
				case ViewStates.Gone:
					view.Visibility = ViewStates.Visible;
					break;
				case ViewStates.Visible:
					view.Visibility = ViewStates.Gone;
					break;
			}
		}
	}
}
