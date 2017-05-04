// Created by Philip Gai

using Android.App;
using Android.Views;
using Android.Widget;

namespace Commercially.Droid
{
	/// <summary>
	/// Linear layout extensions.
	/// </summary>
	public static class LinearLayoutExtensions
	{
		/// <summary>
		/// Adds the view with underline.
		/// </summary>
		/// <param name="layout">Layout.</param>
		/// <param name="view">View.</param>
		/// <param name="activity">Activity.</param>
		public static void AddViewWithUnderline(this LinearLayout layout, View view, Activity activity)
		{
			layout.AddView(view);
			layout.AddView(activity.GetHorizontalLine());
		}
	}
}
