using System;
using UIKit;

namespace Commercially.iOS
{
	public partial class ButtonDetailsController : KeyboardController
	{
		private readonly ButtonDetails SharedController = new ButtonDetails();

		public FlicButton Button {
			set {
				SharedController.Button = value;
			}
		}

		public ButtonDetailsController(IntPtr handle) : base(handle) { }

		bool IsChanged {
			get {
				return SharedController.PickerChanged(ClientPickerView.Model.GetTitle(ClientPickerView, 0, 0)) || SharedController.DescriptionChanged(DescriptionField.Text) || SharedController.LocationChanged(LocationField.Text);
			}
		}

		public override UIScrollView ScrollView {
			get {
				return KeyboardScrollView;
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			InitializeView();
		}

		partial void SaveButtonPress(UIButton sender)
		{
			try {
				SharedController.SaveButtonPress(LocationField.Text, DescriptionField.Text,
												 ClientPickerView.Model.GetTitle(ClientPickerView, 0, 0));
			} catch (Exception) {
				NavigationController.ShowPrompt(Localizable.PromptMessages.ButtonSaveError);
				return;
			}
			UIView.AnimateAsync(ButtonDetails.AnimationDuration, delegate {
				SaveButton.Hidden = true;
			});
			NavigationController.PopViewController(true);
		}

		void InitializeView()
		{
			if (SharedController.Button == null) return;
			LocationField.ShouldReturn += (textField) => { LocationField.ResignFirstResponder(); return true; };
			DescriptionField.ShouldReturn += (textField) => { DescriptionField.ResignFirstResponder(); return true; };
			LocationField.Text = SharedController.LocationFieldText;
			DescriptionField.Text = SharedController.DescriptionFieldText;
			BluetoothIdLabel.Text = SharedController.BluetoothIdText;
			LocationField.EditingChanged += OnFieldChange;
			DescriptionField.EditingChanged += OnFieldChange;
			ClientIdLabel.Hidden = SharedController.ClientIdIsHidden;
			PairStack.Hidden = SharedController.PairStackIsHidden;
			ClientPickerView.Model = new ClientPickerViewModel(SharedController.Button, OnPickerChange);
			ClientIdLabel.Text = SharedController.ClientIdText;
			SaveButton.Hidden = true;
		}

		void OnPickerChange(UIPickerView pickerView, nint row, nint component)
		{
			SharedController.SelectedClient = pickerView.Model.GetTitle(pickerView, row, component);
			UIView.AnimateAsync(ButtonDetails.AnimationDuration, delegate {
				SaveButton.Hidden = !IsChanged;
			});
		}

		void OnFieldChange(object sender, EventArgs e)
		{
			UIView.AnimateAsync(ButtonDetails.AnimationDuration, delegate {
				SaveButton.Hidden = !IsChanged;
			});
		}
	}
}