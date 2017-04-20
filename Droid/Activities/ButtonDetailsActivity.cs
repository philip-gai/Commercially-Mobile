
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Commercially.Droid
{
	[Activity(Label = "ButtonDetailsActivity")]
	public class ButtonDetailsActivity : AppCompatActivity
	{
		readonly ButtonDetails SharedController = new ButtonDetails();

		EditText LocationField { get { return FindViewById<EditText>(Resource.Id.locationField); } }
		EditText DescriptionField { get { return FindViewById<EditText>(Resource.Id.descriptionField); } }
		TextView BluetoothIdText { get { return FindViewById<TextView>(Resource.Id.bluetoothIdText); } }
		TextView ClientIdText { get { return FindViewById<TextView>(Resource.Id.clientIdText); } }
		Button SaveButton { get { return FindViewById<Button>(Resource.Id.saveButton); } }
		LinearLayout PairLayout { get { return FindViewById<LinearLayout>(Resource.Id.pairLayout); } }
		Spinner ClientSpinner { get { return FindViewById<Spinner>(Resource.Id.clientSpinner); } }

		bool IsChanged {
			get {
				return SharedController.PickerChanged(ClientSpinner.GetItemAtPosition(0).ToString()) || SharedController.DescriptionChanged(DescriptionField.Text) || SharedController.LocationChanged(LocationField.Text);
			}
		}

		bool LocationChanged {
			get {
				if (SharedController.Button.room == null) return !string.IsNullOrWhiteSpace(LocationField.Text);
				return !SharedController.Button.room.Equals(LocationField.Text);
			}
		}

		bool DescriptionChanged {
			get {
				if (SharedController.Button.description == null) return !string.IsNullOrWhiteSpace(DescriptionField.Text);
				return !SharedController.Button.description.Equals(DescriptionField.Text);
			}
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.ButtonDetails);
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
			this.ShowBackArrow();

			var button = JsonConvert.DeserializeObject<FlicButton>(Intent.GetStringExtra(typeof(FlicButton).Name));
			SharedController.Button = button;
			InitializeView();
		}

		public override bool OnSupportNavigateUp()
		{
			Finish();
			return true;
		}

		void InitializeView()
		{
			if (SharedController.Button == null) return;
			LocationField.Text = SharedController.LocationFieldText;
			DescriptionField.Text = SharedController.DescriptionFieldText;
			BluetoothIdText.Text = SharedController.BluetoothIdText;

			LocationField.TextChanged += OnFieldTextChange;
			DescriptionField.TextChanged += OnFieldTextChange;

			ClientIdText.Visibility = SharedController.ClientIdIsHidden ? ViewStates.Gone : ViewStates.Visible;
			PairLayout.Visibility = SharedController.PairStackIsHidden ? ViewStates.Gone : ViewStates.Visible;
			// Make the spinner
			//	ClientPickerView.Model = new ClientPickerViewModel(FlicButton.GetDiscoveredByClients(SharedController.Button), OnPickerChange);
			ClientIdText.Text = SharedController.ClientIdText;
			SaveButton.Visibility = ViewStates.Gone;
			ClientSpinner.ItemSelected += OnSpinnerItemSelected;
		}

		void OnSpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			SharedController.SelectedClient = e.ToString();
			SaveButton.Visibility = IsChanged ? ViewStates.Visible : ViewStates.Gone;
		}

		void OnFieldTextChange(object sender, TextChangedEventArgs e)
		{
			SaveButton.Visibility = IsChanged ? ViewStates.Visible : ViewStates.Gone;
		}
	}
}
