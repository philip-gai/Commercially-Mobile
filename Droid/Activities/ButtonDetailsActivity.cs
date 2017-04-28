using System;
using Android.App;
using Android.Content;
using Android.OS;
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
		LinearLayout ClientLayout { get { return FindViewById<LinearLayout>(Resource.Id.clientLayout); } }
		Spinner ClientSpinner;

		bool IsChanged {
			get {
				return SharedController.PickerChanged(ClientSpinner.GetItemAtPosition(0).ToString()) || SharedController.DescriptionChanged(DescriptionField.Text) || SharedController.LocationChanged(LocationField.Text);
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
			ClientIdText.Text = SharedController.ClientIdText;

			ClientLayout.Hidden(SharedController.ClientStackIsHidden);
			PairLayout.Hidden(SharedController.PairStackIsHidden);
			SaveButton.Hidden(true);

			LocationField.TextChanged += OnTextChanged;
			DescriptionField.TextChanged += OnTextChanged;
			SaveButton.Click += SaveButtonClick;

			ClientSpinner = this.GetClientSpinner(SharedController.Button);
			ClientSpinner.ItemSelected += ClientSpinnerItemSelected;
		}

		void ClientSpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			var adapter = (sender as Spinner).Adapter;
			SharedController.SelectedClient = adapter.GetItem(e.Position).ToString();
			SaveButton.Hidden(!IsChanged);
		}

		void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			SaveButton.Hidden(!IsChanged);
		}

		void SaveButtonClick(object sender, EventArgs e)
		{
			try {
				SharedController.SaveButtonPress(LocationField.Text, DescriptionField.Text,
												 ClientSpinner.GetItemAtPosition(0).ToString());
			} catch (Exception) {
				this.ShowPrompt(Localizable.PromptMessages.ChangesSaveError);
				return;
			}
			SaveButton.Hidden(true);
			Finish();
		}
	}
}
