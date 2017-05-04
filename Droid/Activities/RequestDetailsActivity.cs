// Created by Philip Gai

using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Commercially.Droid
{
	/// <summary>
	/// Request details activity.
	/// </summary>
	[Activity(Label = "RequestDetailsActivity")]
	public class RequestDetailsActivity : AppCompatActivity
	{
		readonly RequestDetailsManager Manager = new RequestDetailsManager();

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
			Manager.Request = request;
			InitializeView();
		}

		public override bool OnSupportNavigateUp()
		{
			Finish();
			return true;
		}

		void InitializeView()
		{
			if (Manager.Request == null) return;
			InitializeText();
			InitializeSpinners();
			InitializeVisibility();
			InitializeActions();
		}

		void InitializeText()
		{
			DescriptionText.Text = Manager.DescriptionText;
			LocationText.Text = Manager.LocationText;
			StatusText.Text = Manager.StatusText;
			AssignedToText.Text = Manager.AssignedToText;
			ReceivedTimeText.Text = Manager.ReceivedTimeText;
			AcceptedTimeText.Text = Manager.AcceptedTimeText;
			CompletedTimeText.Text = Manager.CompletedTimeText;
			StaticUserText.SetTextColor(RequestDetailsManager.EditTextColor.GetAndroidColor());
		}

		void InitializeSpinners()
		{
			StatusSpinner = this.GetStatusSpinner();
			StatusSpinner.SetSelection(Manager.StatusText);
			StaticStatusText.SetTextColor(Manager.StatusInputIsHidden ? RequestDetailsManager.DefaultTextColor.GetAndroidColor() : RequestDetailsManager.EditTextColor.GetAndroidColor());
			StatusSpinner.ItemSelected += OnSpinnerItemSelected;

			UserSpinner = this.GetUserSpinner();
			Manager.SelectedUser = Manager.StartingUser;
			UserSpinner.SetSelection(Manager.StartingUser);
			UserSpinner.ItemSelected += OnSpinnerItemSelected;
		}

		void InitializeVisibility()
		{
			UrgentIndicator.Hidden(Manager.UrgentIndicatorIsHidden);
			AssignedToText.Hidden(Manager.AssignedToTextIsHidden);
			AssignButton.Hidden(Manager.AssignButtonIsHidden);
			SaveButton.Hidden(Manager.SaveButtonIsHidden);
			StatusSpinner.Hidden(Manager.StatusInputIsHidden);
			StatusText.Hidden(Manager.StatusLabelIsHidden);
			UserSpinnerLayout.Hidden(Manager.UserPickerStackIsHidden);
		}

		void InitializeActions()
		{
			SaveButton.Click += SaveButtonClick;
			AssignButton.Click += AssignButtonClick;
		}

		void SaveButtonClick(object sender, EventArgs e)
		{
			try {
				Manager.SaveStatusChanges();
				Manager.SaveUserChanges();
			} catch (Exception) {
				this.ShowPrompt(Localizable.PromptMessages.ChangesSaveError);
				return;
			}
			(sender as Button).Hidden(true);
			Finish();
		}

		void AssignButtonClick(object sender, EventArgs e)
		{
			try {
				Manager.OnAssignButtonPressHandler();
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
				Manager.SelectedStatus = adapter.GetItem(e.Position).ToString();
			} else if (sender == UserSpinner) {
				Manager.SelectedUser = adapter.GetItem(e.Position).ToString();
			}
			SaveButton.Hidden(Manager.SaveButtonIsHidden);
			AssignButton.Hidden(Manager.AssignButtonIsHidden);
		}
	}
}