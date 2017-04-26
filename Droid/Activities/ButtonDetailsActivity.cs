using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
			this.InitializeClientSpinner(SharedController.Button);
			ClientIdText.Text = SharedController.ClientIdText;

			ClientIdText.Visibility = SharedController.ClientIdIsHidden ? ViewStates.Gone : ViewStates.Visible;
			PairLayout.Visibility = SharedController.PairStackIsHidden ? ViewStates.Gone : ViewStates.Visible;
			SaveButton.Visibility = ViewStates.Gone;

			ClientSpinner.ItemSelected += ClientSpinnerItemSelected;
			LocationField.TextChanged += OnTextChanged;
			DescriptionField.TextChanged += OnTextChanged;
			SaveButton.Click += SaveButtonClick;
		}

		void ClientSpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			var adapter = (sender as Spinner).Adapter;
			SharedController.SelectedClient = adapter.GetItem(e.Position).ToString();
			SaveButton.Visibility = IsChanged ? ViewStates.Visible : ViewStates.Gone;
		}

		void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			SaveButton.Visibility = IsChanged ? ViewStates.Visible : ViewStates.Gone;
		}

		void SaveButtonClick(object sender, EventArgs e)
		{
			try {
				var jsonBody = new JObject();
				if (SharedController.LocationChanged(LocationField.Text) && LocationField.Text != null) {
					jsonBody.Add("room", LocationField.Text);
				}
				if (SharedController.DescriptionChanged(DescriptionField.Text) && DescriptionField.Text != null) {
					jsonBody.Add("description", DescriptionField.Text);
				}
				if (jsonBody.Count > 0) {
					FlicButtonApi.PatchButton(SharedController.Button.bluetooth_id, jsonBody.ToString());
				}
				if (SharedController.PickerChanged(ClientSpinner.GetItemAtPosition(0).ToString())) {
					FlicButtonApi.PairButton(SharedController.Button.bluetooth_id, Client.FindClient(SharedController.SelectedClient, Session.Clients).clientId);
				}
			} catch (Exception) {
				this.ShowPrompt(Localizable.PromptMessages.ButtonSaveError);
				return;
			}
			SaveButton.Visibility = ViewStates.Gone;
			Finish();
		}
	}
}
