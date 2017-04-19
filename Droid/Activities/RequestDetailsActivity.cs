using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Commercially.Droid
{
	[Activity(Label = "RequestDetailsActivity")]
	public class RequestDetailsActivity : AppCompatActivity
	{
		public readonly RequestDetails SharedController = new RequestDetails();

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.RequestDetails);
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

			var request = JsonConvert.DeserializeObject<Request>(Intent.GetStringExtra(typeof(Request).Name));
			SharedController.Request = request;
			InitializeView();
		}

		void InitializeView()
		{
			if (SharedController.Request == null) return;
			InitializeText();
			InitializeVisibility();
			InitializeSpinner();
		}

		void InitializeText()
		{
			var descriptionText = FindViewById<TextView>(Resource.Id.descriptionText);
			var locationText = FindViewById<TextView>(Resource.Id.locationText);
			var statusText = FindViewById<TextView>(Resource.Id.statusText);
			var assignedToText = FindViewById<TextView>(Resource.Id.assignedToText);
			var receivedTimeText = FindViewById<TextView>(Resource.Id.receivedTimeText);
			var acceptedTimeText = FindViewById<TextView>(Resource.Id.acceptedTimeText);
			var completedTimeText = FindViewById<TextView>(Resource.Id.completedTimeText);

			descriptionText.Text = SharedController.DescriptionText;
			locationText.Text = SharedController.LocationText;
			statusText.Text = SharedController.StatusText;
			assignedToText.Text = SharedController.AssignedToText;
			receivedTimeText.Text = SharedController.ReceivedTimeText;
			acceptedTimeText.Text = SharedController.AcceptedTimeText;
			completedTimeText.Text = SharedController.CompletedTimeText;
		}

		void InitializeVisibility()
		{
			var urgentIndicator = FindViewById(Resource.Id.urgentIndicator);
			var assignedToText = FindViewById<TextView>(Resource.Id.assignedToText);
			var assignButton = FindViewById<Button>(Resource.Id.assignButton);
			var saveButton = FindViewById<Button>(Resource.Id.saveButton);
			var statusSpinner = FindViewById<Spinner>(Resource.Id.statusSpinner);
			var statusText = FindViewById<TextView>(Resource.Id.statusText);

			urgentIndicator.Visibility = SharedController.UrgentIndicatorIsHidden ? ViewStates.Gone : ViewStates.Visible;
			assignedToText.Visibility = SharedController.AssignedToIsHidden ? ViewStates.Gone : ViewStates.Visible;
			assignButton.Visibility = SharedController.AssignButtonIsHidden ? ViewStates.Gone : ViewStates.Visible;
			saveButton.Visibility = SharedController.SaveButtonIsHidden ? ViewStates.Gone : ViewStates.Visible;
			statusSpinner.Visibility = SharedController.StatusInputIsHidden ? ViewStates.Gone : ViewStates.Visible;
			statusText.Visibility = SharedController.StatusLabelIsHidden ? ViewStates.Gone : ViewStates.Visible;
		}

		void InitializeSpinner()
		{
			var staticStatusText = FindViewById<TextView>(Resource.Id.staticStatusText);
			var statusSpinner = FindViewById<Spinner>(Resource.Id.statusSpinner);
			var adapter = new ArrayAdapter(this, Resource.Array.status_array);
			statusSpinner.Adapter = adapter;
			statusSpinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
			};
			staticStatusText.SetTextColor(RequestDetails.StaticStatusDefault.GetAndroidColor());
			if (!SharedController.StatusInputIsHidden) {
				staticStatusText.SetTextColor(RequestDetails.StaticStatusEdit.GetAndroidColor());
			}
		}
	}
}
