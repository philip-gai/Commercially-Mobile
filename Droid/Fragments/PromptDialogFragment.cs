
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Commercially.Droid
{
	public class PromptDialogFragment : DialogFragment
	{
		readonly string Title;

		public PromptDialogFragment (string title)
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
