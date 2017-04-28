// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Commercially.iOS
{
    [Register ("ButtonDetailsController")]
    partial class ButtonDetailsController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel BluetoothIdLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ClientIdLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView ClientPickerView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStackView ClientStack { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField DescriptionField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView KeyboardScrollView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField LocationField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStackView PairStack { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SaveButton { get; set; }

        [Action ("SaveButtonPress:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SaveButtonPress (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (BluetoothIdLabel != null) {
                BluetoothIdLabel.Dispose ();
                BluetoothIdLabel = null;
            }

            if (ClientIdLabel != null) {
                ClientIdLabel.Dispose ();
                ClientIdLabel = null;
            }

            if (ClientPickerView != null) {
                ClientPickerView.Dispose ();
                ClientPickerView = null;
            }

            if (ClientStack != null) {
                ClientStack.Dispose ();
                ClientStack = null;
            }

            if (DescriptionField != null) {
                DescriptionField.Dispose ();
                DescriptionField = null;
            }

            if (KeyboardScrollView != null) {
                KeyboardScrollView.Dispose ();
                KeyboardScrollView = null;
            }

            if (LocationField != null) {
                LocationField.Dispose ();
                LocationField = null;
            }

            if (PairStack != null) {
                PairStack.Dispose ();
                PairStack = null;
            }

            if (SaveButton != null) {
                SaveButton.Dispose ();
                SaveButton = null;
            }
        }
    }
}