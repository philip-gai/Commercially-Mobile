// Created by Philip Gai

using System;
using UIKit;

namespace Commercially.iOS
{
	/// <summary>
	/// Request details controller.
	/// </summary>
	public partial class RequestDetailsController : UIViewController
	{
		readonly RequestDetailsManager Manager = new RequestDetailsManager();

		public Request Request {
			set {
				Manager.Request = value;
			}
		}

		public RequestDetailsController(IntPtr handle) : base(handle) { }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			InitializeView();
		}

		void InitializeView()
		{
			if (Manager.Request == null) return;
			InitializeText();
			InitializeVisibility();
			InitializeStatusPickers();
		}

		void InitializeText()
		{
			DescriptionLabel.Text = Manager.DescriptionText;
			RoomLabel.Text = Manager.LocationText;
			StatusLabel.Text = Manager.StatusText;
			AssignedToLabel.Text = Manager.AssignedToText;
			ReceivedTimeLabel.Text = Manager.ReceivedTimeText;
			AcceptedTimeLabel.Text = Manager.AcceptedTimeText;
			CompletedTimeLabel.Text = Manager.CompletedTimeText;
			StaticAssignLabel.TextColor = RequestDetailsManager.EditTextColor.GetUIColor();
		}

		void InitializeVisibility()
		{
			UrgentIndicator.Hidden = Manager.UrgentIndicatorIsHidden;
			AssignedToLabel.Hidden = Manager.AssignedToTextIsHidden;
			AssignButton.Hidden = Manager.AssignButtonIsHidden;
			SaveButton.Hidden = Manager.SaveButtonIsHidden;
			ButtonStackView.Hidden = Manager.ButtonStackIsHidden;
			StatusPickerView.Hidden = Manager.StatusInputIsHidden;
			StatusLabel.Hidden = Manager.StatusLabelIsHidden;
			AssignPickerStack.Hidden = Manager.UserPickerStackIsHidden;
		}

		void InitializeStatusPickers()
		{
			StatusPickerView.Model = new StatusPickerModel(PickerSelected);
			StatusPickerView.ScrollToTitle(Manager.Request.Type.ToString());
			StaticStatusLabel.TextColor = Manager.StatusInputIsHidden ? RequestDetailsManager.DefaultTextColor.GetUIColor() : RequestDetailsManager.EditTextColor.GetUIColor();

			UserPicker.Model = new UserPickerViewModel(PickerSelected);
			Manager.SelectedUser = Manager.StartingUser;
			UserPicker.ScrollToTitle(Manager.SelectedUser);
		}

		partial void SaveButtonPress(UIButton sender)
		{
			// Call Post to change request status string
			// Post for time that status was changed
			try {
				Manager.SaveStatusChanges();
				Manager.SaveUserChanges();
			} catch (Exception) {
				NavigationController.ShowPrompt(Localizable.PromptMessages.ChangesSaveError);
				return;
			}

			ButtonStackView.Hidden = AssignButton.Hidden;
			SaveButton.Hidden = true;
			NavigationController.PopViewController(true);
		}

		partial void AssignButtonPress(UIButton sender)
		{
			try {
				Manager.OnAssignButtonPressHandler();
			} catch (Exception) {
				NavigationController.ShowPrompt(Localizable.PromptMessages.AssignError);
				return;
			}

			UIView.AnimateAsync(RequestDetailsManager.AnimationDuration, delegate {
				ButtonStackView.Hidden = SaveButton.Hidden;
			});
			AssignButton.Hidden = true;
			NavigationController.PopViewController(true);
		}

		void PickerSelected(UIPickerView pickerView, nint row, nint component)
		{
			if (pickerView == StatusPickerView) {
				Manager.SelectedStatus = pickerView.Model.GetTitle(pickerView, row, component);
			} else if (pickerView == UserPicker) {
				Manager.SelectedUser = pickerView.Model.GetTitle(pickerView, row, component);
			}
			SaveButton.Hidden = Manager.SaveButtonIsHidden;
			AssignButton.Hidden = Manager.AssignButtonIsHidden;
			UIView.AnimateAsync(RequestDetailsManager.AnimationDuration, delegate {
				ButtonStackView.Hidden = Manager.ButtonStackIsHidden;
			});
		}
	}
}