// Created by Philip Gai

using Android.Views;

namespace Commercially.Droid
{
	/// <summary>
	/// View extensions.
	/// </summary>
	public static class ViewExtensions
	{
		/// <summary>
		/// Shows or hides the specified view.
		/// </summary>
		/// <param name="view">The view to hide or show.</param>
		/// <param name="hide">If set to <c>true</c> hide.</param>
		/// <param name="useGone">If set to <c>true</c> use gone instead of invisible.</param>
		public static void Hidden(this View view, bool hide, bool useGone = true)
		{
			view.Visibility = hide ? (useGone ? ViewStates.Gone : ViewStates.Invisible) : ViewStates.Visible;
		}

		/// <summary>
		/// Toggles the visibility of the view.
		/// </summary>
		/// <param name="view">View.</param>
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
