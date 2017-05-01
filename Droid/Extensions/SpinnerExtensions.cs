using Android.Widget;

namespace Commercially.Droid
{
	public static class SpinnerExtensions
	{
		public static void SetSelection(this Spinner spinner, string title)
		{
			spinner.SetSelection(((ArrayAdapter)spinner.Adapter).GetPosition(title));
		}
	}
}
