using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Android.Animation;
using Newtonsoft.Json;
using System;
using Android.Support.V4.App;

namespace Commercially.Droid
{
	[Activity(Label = "RequestDetailsActivity")]
	public class RequestDetailsActivity : AppCompatActivity
	{
		readonly RequestDetails SharedController = new RequestDetails();

		TextView DescriptionText { get { return FindViewById<TextView>(Resource.Id.descriptionText); } }
		TextView LocationText { get { return FindViewById<TextView>(Resource.Id.locationText); } }
		TextView StatusText { get { return FindViewById<TextView>(Resource.Id.statusText); } }
		TextView StaticStatusText { get { return FindViewById<TextView>(Resource.Id.staticStatusText); } }
		TextView AssignedToText { get { return FindViewById<TextView>(Resource.Id.assignedToText); } }
		TextView ReceivedTimeText { get { return FindViewById<TextView>(Resource.Id.receivedTimeText); } }
		TextView AcceptedTimeText { get { return FindViewById<TextView>(Resource.Id.acceptedTimeText); } }
		TextView CompletedTimeText { get { return FindViewById<TextView>(Resource.Id.completedTimeText); } }
		ImageView UrgentIndicator { get { return FindViewById<ImageView>(Resource.Id.urgentIndicator); } }
		Button AssignButton { get { return FindViewById<Button>(Resource.Id.assignButton); } }
		Button SaveButton { get { return FindViewById<Button>(Resource.Id.saveButton); } }
		Spinner StatusSpinner { get { return FindViewById<Spinner>(Resource.Id.statusSpinner); } }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.RequestDetails);
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
			this.ShowBackArrow();

			var request = JsonConvert.DeserializeObject<Request>(Intent.GetStringExtra(typeof(Request).Name));
			SharedController.Request = request;
			InitializeView();
		}

		public override bool OnSupportNavigateUp()
		{
			Finish();
			return true;
		}

		void InitializeView()
		{
			if (SharedController.Request == null) return;
			InitializeText();
			InitializeVisibility();
			InitializeSpinner();
			InitializeActions();
			StatusSpinner.SetSelection(((ArrayAdapter)StatusSpinner.Adapter).GetPosition(SharedController.StatusText));
		}

		void InitializeText()
		{
			DescriptionText.Text = SharedController.DescriptionText;
			LocationText.Text = SharedController.LocationText;
			StatusText.Text = SharedController.StatusText;
			AssignedToText.Text = SharedController.AssignedToText;
			ReceivedTimeText.Text = SharedController.ReceivedTimeText;
			AcceptedTimeText.Text = SharedController.AcceptedTimeText;
			CompletedTimeText.Text = SharedController.CompletedTimeText;
		}

		void InitializeVisibility()
		{
			UrgentIndicator.Visibility = SharedController.UrgentIndicatorIsHidden ? ViewStates.Gone : ViewStates.Visible;
			AssignedToText.Visibility = SharedController.AssignedToIsHidden ? ViewStates.Gone : ViewStates.Visible;
			AssignButton.Visibility = SharedController.AssignButtonIsHidden ? ViewStates.Gone : ViewStates.Visible;
			SaveButton.Visibility = SharedController.SaveButtonIsHidden ? ViewStates.Gone : ViewStates.Visible;
			StatusSpinner.Visibility = SharedController.StatusInputIsHidden ? ViewStates.Gone : ViewStates.Visible;
			StatusText.Visibility = SharedController.StatusLabelIsHidden ? ViewStates.Gone : ViewStates.Visible;
		}

		void InitializeSpinner()
		{
			this.InitializeStatusSpinner();
			StaticStatusText.SetTextColor(RequestDetails.StaticStatusDefault.GetAndroidColor());
			if (!SharedController.StatusInputIsHidden) {
				StaticStatusText.SetTextColor(RequestDetails.StaticStatusEdit.GetAndroidColor());
			}
		}

		void InitializeActions()
		{
			StatusSpinner.ItemSelected += OnSpinnerItemSelected;
			SaveButton.Click += SaveButtonClick;
			AssignButton.Click += AssignButtonClick;
		}

		void OnSpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			var adapter = (sender as Spinner).Adapter;
			SharedController.SelectedStatus = adapter.GetItem(e.Position).ToString();
			SaveButton.Visibility = SharedController.SaveButtonIsHidden ? ViewStates.Gone : ViewStates.Visible;
		}

		void SaveButtonClick(object sender, EventArgs e)
		{
			var saveButton = sender as Button;
			try {
				RequestApi.UpdateRequest(SharedController.Request._id, Request.GetStatusType(SharedController.SelectedStatus));
			} catch (Exception) {
				this.ShowPrompt(Localizable.PromptMessages.RequestSaveError);
				return;
			}
			saveButton.Visibility = ViewStates.Gone;
			Finish();
		}

		void AssignButtonClick(object sender, EventArgs e)
		{
			var assignButton = sender as Button;
			// Call Post to change button ownedBy value to this user's email in DB
			try {
				RequestApi.UpdateRequest(SharedController.Request._id, RequestStatusType.Assigned);
			} catch (Exception) {
				this.ShowPrompt(Localizable.PromptMessages.AssignError);
				return;
			}

			assignButton.Visibility = ViewStates.Gone;
			Finish();
		}
	}
}