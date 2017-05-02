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
    [Register ("UserDetailsController")]
    partial class UserDetailsController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ChangePasswordButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField EmailField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField NameField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField NewPasswordField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField OldPasswordField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField PhoneField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField RepeatNewPasswordField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SaveButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ChangePasswordButton != null) {
                ChangePasswordButton.Dispose ();
                ChangePasswordButton = null;
            }

            if (EmailField != null) {
                EmailField.Dispose ();
                EmailField = null;
            }

            if (NameField != null) {
                NameField.Dispose ();
                NameField = null;
            }

            if (NewPasswordField != null) {
                NewPasswordField.Dispose ();
                NewPasswordField = null;
            }

            if (OldPasswordField != null) {
                OldPasswordField.Dispose ();
                OldPasswordField = null;
            }

            if (PhoneField != null) {
                PhoneField.Dispose ();
                PhoneField = null;
            }

            if (RepeatNewPasswordField != null) {
                RepeatNewPasswordField.Dispose ();
                RepeatNewPasswordField = null;
            }

            if (SaveButton != null) {
                SaveButton.Dispose ();
                SaveButton = null;
            }
        }
    }
}