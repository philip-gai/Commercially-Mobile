// Created by Philip Gai

using System;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// Button details controller.
	/// </summary>
	public partial class ButtonDetailsController : KeyboardController
	{
		readonly ButtonDetailsManager Manager = new ButtonDetailsManager();

		public ButtonDetailsController(IntPtr handle) : base(handle) { }

		public FlicButton Button {
			set {
				Manager.Button = value;
			}
		}

		bool DetailsAreChanged {
			get {
				return Manager.PickerDidChange(ClientPickerView.Model.GetTitle(ClientPickerView, 0, 0)) || Manager.DescriptionIsEdited(DescriptionField.Text) || Manager.LocationIsEdited(LocationField.Text);
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
				if (Manager.OnSaveButtonPressHandler(LocationField.Text, DescriptionField.Text,
													ClientPickerView.Model.GetTitle(ClientPickerView, 0, 0)) == true) {
					NavigationController.ShowPrompt(Localizable.PromptMessages.PressAndHoldButton);
				}
			} catch (Exception) {
				NavigationController.ShowPrompt(Localizable.PromptMessages.ChangesSaveError);
				return;
			}

			SaveButton.Hidden = true;
			NavigationController.PopViewController(true);
		}

		void InitializeView()
		{
			if (Manager.Button == null) return;

			LocationField.Text = Manager.LocationFieldText;
			DescriptionField.Text = Manager.DescriptionFieldText;
			BluetoothIdLabel.Text = Manager.BluetoothIdText;

			LocationField.ResignOnReturn();
			DescriptionField.ResignOnReturn();
			LocationField.EditingDidEnd += FieldEditingDidEnd;
			DescriptionField.EditingDidEnd += FieldEditingDidEnd;

			ClientStack.Hidden = Manager.ClientStackIsHidden;
			PairStack.Hidden = Manager.PairStackIsHidden;

			ClientPickerView.Model = new ClientPickerViewModel(Manager.Button, PickerSelected);
			ClientIdLabel.Text = Manager.ClientIdText;
			SaveButton.Hidden = true;
		}

		void PickerSelected(UIPickerView pickerView, nint row, nint component)
		{
			Manager.SelectedClient = pickerView.Model.GetTitle(pickerView, row, component);
			UIView.AnimateAsync(ButtonDetailsManager.AnimationDuration, delegate {
				SaveButton.Hidden = !DetailsAreChanged;
			});
		}

		void FieldEditingDidEnd(object sender, EventArgs e)
		{
			UIView.AnimateAsync(ButtonDetailsManager.AnimationDuration, delegate {
				SaveButton.Hidden = !DetailsAreChanged;
			});
		}
	}
}