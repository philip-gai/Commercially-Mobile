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
    [Register ("ClientDetailsController")]
    partial class ClientDetailsController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AuthorizeButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStackView ButtonStack { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField FriendlyNameField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel IdLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIScrollView KeyboardScrollView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SaveButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AuthorizeButton != null) {
                AuthorizeButton.Dispose ();
                AuthorizeButton = null;
            }

            if (ButtonStack != null) {
                ButtonStack.Dispose ();
                ButtonStack = null;
            }

            if (FriendlyNameField != null) {
                FriendlyNameField.Dispose ();
                FriendlyNameField = null;
            }

            if (IdLabel != null) {
                IdLabel.Dispose ();
                IdLabel = null;
            }

            if (KeyboardScrollView != null) {
                KeyboardScrollView.Dispose ();
                KeyboardScrollView = null;
            }

            if (SaveButton != null) {
                SaveButton.Dispose ();
                SaveButton = null;
            }
        }
    }
}