
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
		public static AlertDialogFragment NewInstance(int title) {
			var fragment = new AlertDialogFragment();
			Bundle args = new Bundle();
			args.PutInt("title", title);
			fragment.Arguments = args;
			return fragment;
		}

		public override Dialog OnCreateDialog(Bundle savedInstanceState)
		{
			return new AlertDialog.Builder(Activity)
								  .Create();
		}
	}
}
