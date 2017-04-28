
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
	[Activity(Label = "UserDetailsActivity")]
	public class UserDetailsActivity : AppCompatActivity
	{
		readonly UserDetails SharedController = new UserDetails();

		LinearLayout Layout { get { return FindViewById<LinearLayout>(Resource.Id.mainLayout); } }
		TextView FirstLastText { get { return FindViewById<TextView>(Resource.Id.firstLastText); } }
		TextView EmailText { get { return FindViewById<TextView>(Resource.Id.emailText); } }
		TextView PhoneText { get { return FindViewById<TextView>(Resource.Id.phoneText); } }

		LinearLayout _Table;
		LinearLayout Table {
			get {
				if (_Table != null) return _Table;
				var inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
				var table = (LinearLayout)inflater.Inflate(Resource.Layout.Table, null);
				_Table = table;
				return _Table;
			}
		}


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.UserDetails);
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
			this.ShowBackArrow();

			var user = JsonConvert.DeserializeObject<User>(Intent.GetStringExtra(typeof(User).Name));
			SharedController.User = user;
			InitializeView();
		}

		protected override void OnResume()
		{
			base.OnResume();
			GetRequests();
		}

		public override bool OnSupportNavigateUp()
		{
			Finish();
			return true;
		}

		void InitializeView()
		{
			if (SharedController.User == null) return;
			FirstLastText.Text = SharedController.FirstLastText;
			EmailText.Text = SharedController.EmailText;
			PhoneText.Text = SharedController.PhoneText;
			PhoneText.Hidden(SharedController.PhoneLabelIsHidden);
			Layout.AddView(Table);
		}

		void InitializeTable()
		{
			Table.RemoveAllViews();
			var header = GetHeader();
			Table.AddView(header);
			for (int row = 0; row < SharedController.Requests.Length; row++) {
				var tableRow = GetTableRow(row);
				Table.AddView(tableRow);
			}
		}

		View GetHeader()
		{
			string label = UserDetails.HeaderTitle;
			if (SharedController.Requests != null) {
				label += " (" + SharedController.Requests.Length + ")";
			}
			var header = this.GetSectionHeader(label);
			header.SetBackgroundColor(UserDetails.TableHeaderColor.GetAndroidColor());
			return header;
		}

		TableRow GetTableRow(int row)
		{
			var rowView = this.GetTableRow(SharedController.Requests[row]);
			rowView.SetBackgroundColor(UserDetails.TableHeaderColor.ColorWithAlpha(RequestList.RowAlphaByte));
			return rowView;
		}

		public void GetRequests()
		{
			SharedController.GetRequests(
				delegate { RunOnUiThread(delegate { InitializeTable(); }); },
				(Exception e) => { RunOnUiThread(delegate { this.ShowPrompt(Localizable.PromptMessages.RequestsError); }); }
			);
		}
	}
}
