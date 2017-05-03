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
    [Register ("UserCreateController")]
    partial class UserCreateController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CreateButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField EmailField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView KeyboardScrollView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField NameField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField PasswordField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField PhoneField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField UsernameField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView UserRoleTypePicker { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField VerifyPasswordField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CreateButton != null) {
                CreateButton.Dispose ();
                CreateButton = null;
            }

            if (EmailField != null) {
                EmailField.Dispose ();
                EmailField = null;
            }

            if (KeyboardScrollView != null) {
                KeyboardScrollView.Dispose ();
                KeyboardScrollView = null;
            }

            if (NameField != null) {
                NameField.Dispose ();
                NameField = null;
            }

            if (PasswordField != null) {
                PasswordField.Dispose ();
                PasswordField = null;
            }

            if (PhoneField != null) {
                PhoneField.Dispose ();
                PhoneField = null;
            }

            if (UsernameField != null) {
                UsernameField.Dispose ();
                UsernameField = null;
            }

            if (UserRoleTypePicker != null) {
                UserRoleTypePicker.Dispose ();
                UserRoleTypePicker = null;
            }

            if (VerifyPasswordField != null) {
                VerifyPasswordField.Dispose ();
                VerifyPasswordField = null;
            }
        }
    }
}