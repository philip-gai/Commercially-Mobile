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
	/// Client details activity.
	/// </summary>
	[Activity(Label = "ClientDetailsActivity")]
	public class ClientDetailsActivity : AppCompatActivity
	{
		readonly ClientDetailsManager Manager = new ClientDetailsManager();

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
			Manager.Client = client;
			InitializeView();
		}

		public override bool OnSupportNavigateUp()
		{
			Finish();
			return true;
		}

		void InitializeView()
		{
			if (Manager.Client == null) return;

			IdText.Text = Manager.IdText;
			FriendlyNameField.Text = Manager.FriendlyNameText;
			FriendlyNameField.Hint = Manager.FriendlyNameFieldPlaceholder;

			SaveButton.Hidden(true);
			AuthorizeButton.Hidden(Manager.AuthorizeButtonIsHidden);

			FriendlyNameField.TextChanged += FieldTextChanged;
			SaveButton.Click += SaveButtonClick;
			AuthorizeButton.Click += AuthorizeButtonClick;
		}

		void FieldTextChanged(object sender, TextChangedEventArgs e)
		{
			SaveButton.Hidden(!Manager.FriendlyNameIsChanged(FriendlyNameField.Text));
		}

		void SaveButtonClick(object sender, EventArgs e)
		{
			try {
				Manager.OnSaveButtonPressHandler(FriendlyNameField.Text);
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
				Manager.OnAuthorizeButtonPressHandler();
			} catch (Exception) {
				this.ShowPrompt(Localizable.PromptMessages.AuthorizeError);
				return;
			}
			AuthorizeButton.Hidden(true);
			Finish();
		}
	}
}