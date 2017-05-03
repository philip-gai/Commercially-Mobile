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
		TextView StaticUserText { get { return FindViewById<TextView>(Resource.Id.staticUserText); } }
		TextView AssignedToText { get { return FindViewById<TextView>(Resource.Id.assignedToText); } }
		TextView ReceivedTimeText { get { return FindViewById<TextView>(Resource.Id.receivedTimeText); } }
		TextView AcceptedTimeText { get { return FindViewById<TextView>(Resource.Id.acceptedTimeText); } }
		TextView CompletedTimeText { get { return FindViewById<TextView>(Resource.Id.completedTimeText); } }
		ImageView UrgentIndicator { get { return FindViewById<ImageView>(Resource.Id.urgentIndicator); } }
		Button AssignButton { get { return FindViewById<Button>(Resource.Id.assignButton); } }
		Button SaveButton { get { return FindViewById<Button>(Resource.Id.saveButton); } }
		LinearLayout UserSpinnerLayout { get { return FindViewById<LinearLayout>(Resource.Id.userSpinnerLayout); } }
		Spinner StatusSpinner;
		Spinner UserSpinner;

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
			InitializeSpinners();
			InitializeVisibility();
			InitializeActions();
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
			StaticUserText.SetTextColor(RequestDetails.EditTextColor.GetAndroidColor());
		}

		void InitializeSpinners()
		{
			StatusSpinner = this.GetStatusSpinner();
			StatusSpinner.SetSelection(SharedController.StatusText);
			StaticStatusText.SetTextColor(SharedController.StatusInputIsHidden ? RequestDetails.DefaultTextColor.GetAndroidColor() : RequestDetails.EditTextColor.GetAndroidColor());
			StatusSpinner.ItemSelected += OnSpinnerItemSelected;

			UserSpinner = this.GetUserSpinner();
			SharedController.SelectedUser = SharedController.StartingUser;
			UserSpinner.SetSelection(SharedController.StartingUser);
			UserSpinner.ItemSelected += OnSpinnerItemSelected;
		}

		void InitializeVisibility()
		{
			UrgentIndicator.Hidden(SharedController.UrgentIndicatorIsHidden);
			AssignedToText.Hidden(SharedController.AssignedToIsHidden);
			AssignButton.Hidden(SharedController.AssignButtonIsHidden);
			SaveButton.Hidden(SharedController.SaveButtonIsHidden);
			StatusSpinner.Hidden(SharedController.StatusInputIsHidden);
			StatusText.Hidden(SharedController.StatusLabelIsHidden);
			UserSpinnerLayout.Hidden(SharedController.UserPickerStackIsHidden);
		}

		void InitializeActions()
		{
			SaveButton.Click += SaveButtonClick;
			AssignButton.Click += AssignButtonClick;
		}

		void SaveButtonClick(object sender, EventArgs e)
		{
			try {
				SharedController.SaveStatusChanges();
				SharedController.SaveUserChanges();
			} catch (Exception) {
				this.ShowPrompt(Localizable.PromptMessages.ChangesSaveError);
				return;
			}
			(sender as Button).Hidden(true);
			Finish();
		}

		void AssignButtonClick(object sender, EventArgs e)
		{
			// Call Post to change button ownedBy value to this user's email in DB
			try {
				SharedController.AssignButtonPress();
			} catch (Exception) {
				this.ShowPrompt(Localizable.PromptMessages.AssignError);
				return;
			}

			(sender as Button).Hidden(true);
			Finish();
		}

		void OnSpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			var adapter = (sender as Spinner).Adapter;
			if (sender == StatusSpinner) {
				SharedController.SelectedStatus = adapter.GetItem(e.Position).ToString();
			} else if (sender == UserSpinner) {
				SharedController.SelectedUser = adapter.GetItem(e.Position).ToString();
			}
			SaveButton.Hidden(SharedController.SaveButtonIsHidden);
			AssignButton.Hidden(SharedController.AssignButtonIsHidden);
		}
	}
}