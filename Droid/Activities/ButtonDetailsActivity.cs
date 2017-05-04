// Created by Philip Gai

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
	/// <summary>
	/// Button details activity.
	/// </summary>
	[Activity(Label = "ButtonDetailsActivity")]
	public class ButtonDetailsActivity : AppCompatActivity
	{
		readonly ButtonDetailsManager Manager = new ButtonDetailsManager();

		EditText LocationField { get { return FindViewById<EditText>(Resource.Id.locationField); } }
		EditText DescriptionField { get { return FindViewById<EditText>(Resource.Id.descriptionField); } }
		TextView BluetoothIdText { get { return FindViewById<TextView>(Resource.Id.bluetoothIdText); } }
		TextView ClientIdText { get { return FindViewById<TextView>(Resource.Id.clientIdText); } }
		Button SaveButton { get { return FindViewById<Button>(Resource.Id.saveButton); } }
		LinearLayout PairLayout { get { return FindViewById<LinearLayout>(Resource.Id.pairLayout); } }
		LinearLayout ClientLayout { get { return FindViewById<LinearLayout>(Resource.Id.clientLayout); } }
		Spinner ClientSpinner;

		// Determines if the buttons details have changed
		bool DetailsAreChanged {
			get {
				return Manager.PickerDidChange(ClientSpinner.GetItemAtPosition(0).ToString()) || Manager.DescriptionIsEdited(DescriptionField.Text) || Manager.LocationIsEdited(LocationField.Text);
			}
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.ButtonDetails);
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
			this.ShowBackArrow();

			// Get the passed button
			var button = JsonConvert.DeserializeObject<FlicButton>(Intent.GetStringExtra(typeof(FlicButton).Name));
			Manager.Button = button;
			InitializeView();
		}

		public override bool OnSupportNavigateUp()
		{
			Finish();
			return true;
		}

		void InitializeView()
		{
			if (Manager.Button == null) return;

			LocationField.Text = Manager.LocationFieldText;
			DescriptionField.Text = Manager.DescriptionFieldText;
			BluetoothIdText.Text = Manager.BluetoothIdText;
			ClientIdText.Text = Manager.ClientIdText;

			ClientLayout.Hidden(Manager.ClientStackIsHidden);
			PairLayout.Hidden(Manager.PairStackIsHidden);
			SaveButton.Hidden(true);

			LocationField.TextChanged += FieldTextChanged;
			DescriptionField.TextChanged += FieldTextChanged;
			SaveButton.Click += SaveButtonClick;

			ClientSpinner = this.GetClientSpinner(Manager.Button);
			ClientSpinner.ItemSelected += ClientSpinnerItemSelected;
		}

		void ClientSpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
		{
			var adapter = (sender as Spinner).Adapter;
			Manager.SelectedClient = adapter.GetItem(e.Position).ToString();
			SaveButton.Hidden(!DetailsAreChanged);
		}

		void FieldTextChanged(object sender, TextChangedEventArgs e)
		{
			SaveButton.Hidden(!DetailsAreChanged);
		}

		void SaveButtonClick(object sender, EventArgs e)
		{
			try {
				if (Manager.OnSaveButtonPressHandler(LocationField.Text, DescriptionField.Text,
													ClientSpinner.GetItemAtPosition(0).ToString()) == true) {
					this.ShowPrompt(Localizable.PromptMessages.PressAndHoldButton);
				}
			} catch (Exception) {
				this.ShowPrompt(Localizable.PromptMessages.ChangesSaveError);
				return;
			}
			SaveButton.Hidden(true);
			Finish();
		}
	}
}