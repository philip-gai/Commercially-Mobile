
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
	public class AlertDialogFragment : DialogFragment
	{
		string title;

		public static AlertDialogFragment newInstance(string title) {
			var fragment = new AlertDialogFragment();
			var args = new Bundle();
			args.PutString("title", title);
			fragment.Arguments = args;
			return fragment;
		}

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			title = Arguments.GetString("title");
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var prompt = inflater.Inflate(Resource.Layout.Prompt, container, false);
			var promptMessage = prompt.FindViewById<TextView>(Resource.Id.promptMessage);
			var dismissButton = prompt.FindViewById<Button>(Resource.Id.dismissButton);

			promptMessage.Text = title;
			dismissButton.Click += (sender, e) => {
				Dialog.Show();
			};

			return prompt;
		}
		
	}
}
