
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

		void InitializeView()
		{
			if (SharedController.Button == null) return;
			LocationField.Text = SharedController.LocationFieldText;
			DescriptionField.Text = SharedController.DescriptionFieldText;
			BluetoothIdText.Text = SharedController.BluetoothIdText;

			LocationField.TextChanged += OnFieldChange;
			DescriptionField.TextChanged += OnFieldChange;

			//ClientIdLabel.Hidden = SharedController.ClientIdIsHidden;
			//PairStack.Hidden = SharedController.PairStackIsHidden;
			//if (!PairStack.Hidden) {
			//	ClientPickerView.Model = new ClientPickerViewModel(FlicButton.GetDiscoveredByClients(SharedController.Button), OnPickerChange);
			//}
			//if (!ClientIdLabel.Hidden) {
			//	ClientIdLabel.Text = SharedController.ClientIdText;
			//}
			//SaveButton.Hidden = true;
		}

		void OnFieldChange(object sender, TextChangedEventArgs e)
		{

		}
	}
}
