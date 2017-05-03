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
	[Activity(Label = "ClientDetailsActivity")]
	public class ClientDetailsActivity : AppCompatActivity
	{
		readonly ClientDetails SharedController = new ClientDetails();

		TextView IdText { get { return FindViewById<TextView>(Resource.Id.clientIdText); } }
		EditText FriendlyNameField { get { return FindViewById<EditText>(Resource.Id.friendlyNameField); } }
		Button SaveButton { get { return FindViewById<Button>(Resource.Id.saveButton); } }
		Button AuthorizeButton { get { return FindViewById<Button>(Resource.Id.authorizeButton); } }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.ClientDetails);
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
			this.ShowBackArrow();

			var client = JsonConvert.DeserializeObject<Client>(Intent.GetStringExtra(typeof(Client).Name));
			SharedController.Client = client;
			InitializeView();
		}

		public override bool OnSupportNavigateUp()
		{
			Finish();
			return true;
		}

		void InitializeView()
		{
			if (SharedController.Client == null) return;

			IdText.Text = SharedController.IdText;
			FriendlyNameField.Text = SharedController.FriendlyNameText;
			FriendlyNameField.Hint = SharedController.FriendlyNameFieldPlaceholder;

			SaveButton.Hidden(true);
			AuthorizeButton.Hidden(SharedController.AuthorizeButtonIsHidden);

			FriendlyNameField.TextChanged += OnTextChanged;
			SaveButton.Click += SaveButtonClick;
			AuthorizeButton.Click += AuthorizeButtonClick;
		}

		void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			SaveButton.Hidden(!SharedController.FriendlyNameChanged(FriendlyNameField.Text));
		}

		void SaveButtonClick(object sender, EventArgs e)
		{
			try {
				SharedController.SaveButtonPress(FriendlyNameField.Text);
			} catch (Exception) {
				this.ShowPrompt(Localizable.PromptMessages.ChangesSaveError);
				return;
			}
			SaveButton.Hidden(true);
			Finish();
		}

		void AuthorizeButtonClick(object sender, EventArgs e)
		{
			try {
				SharedController.AuthorizeButtonPress();
			} catch (Exception) {
				this.ShowPrompt(Localizable.PromptMessages.AuthorizeError);
				return;
			}
			AuthorizeButton.Hidden(true);
			Finish();
		}
	}
}