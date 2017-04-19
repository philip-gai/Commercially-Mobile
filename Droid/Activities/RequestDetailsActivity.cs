
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using Newtonsoft.Json;

namespace Commercially.Droid
{
	[Activity(Label = "RequestDetailsActivity")]
	public class RequestDetailsActivity : AppCompatActivity
	{
		Request Request {
			set {
				this.SetRequestDetails(value);
			}
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.RequestDetails);
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

			Request = JsonConvert.DeserializeObject<Request>(Intent.GetStringExtra(typeof(Request).Name));
		}
	}
}
