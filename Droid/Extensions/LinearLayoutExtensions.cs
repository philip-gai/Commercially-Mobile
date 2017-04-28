using Android.App;
using Android.Views;
using Android.Widget;

namespace Commercially.Droid
{
	public static class LinearLayoutExtensions
	{
		public static void AddViewWithUnderline(this LinearLayout layout, View view, Activity activity)
		{
			layout.AddView(view);
			layout.AddView(activity.GetHorizontalLine());
		}
	}
}
