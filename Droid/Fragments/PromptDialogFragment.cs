// Created by Philip Gai

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Commercially.Droid
{
	/// <summary>
	/// Prompt dialog fragment.
	/// </summary>
	public class PromptDialogFragment : DialogFragment
	{
		readonly string Title;

		public PromptDialogFragment(string title)
		{
			Title = title;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var prompt = inflater.Inflate(Resource.Layout.Prompt, container, false);
			var promptMessage = prompt.FindViewById<TextView>(Resource.Id.promptMessage);
			var dismissButton = prompt.FindViewById<Button>(Resource.Id.dismissButton);

			promptMessage.Text = Title;

			dismissButton.Click += (sender, e) => {
				Dismiss();
			};

			return prompt;
		}
	}
}
