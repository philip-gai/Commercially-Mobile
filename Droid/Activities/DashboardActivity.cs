
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Commercially.Droid
{
	[Activity(Label = "DashboardActivity")]
	public class DashboardActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Dashboard);

			// Create your application here
			Initialize();
		}

		void Initialize()
		{
			var table = FindViewById<TableLayout>(Resource.Id.tableLayout);
			var inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
			var rowView = (TableRow)inflater.Inflate(Resource.Layout.RequestRow, null);
			var description = rowView.FindViewById<TextView>(Resource.Id.descriptionText);
			description.Text = "Hey there";
			table.AddView(rowView, 0);
		}
	}
}
