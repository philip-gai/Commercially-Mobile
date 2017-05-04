// Created by Philip Gai

using Android.Widget;

namespace Commercially.Droid
{
	/// <summary>
	/// Spinner extensions.
	/// </summary>
	public static class SpinnerExtensions
	{
		/// <summary>
		/// Sets the spinner's selection.
		/// </summary>
		/// <param name="spinner">Spinner.</param>
		/// <param name="title">Title.</param>
		public static void SetSelection(this Spinner spinner, string title)
		{
			spinner.SetSelection(((ArrayAdapter)spinner.Adapter).GetPosition(title));
		}
	}
}
