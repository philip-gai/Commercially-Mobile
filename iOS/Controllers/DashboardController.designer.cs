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
    [Register ("DashboardController")]
    partial class DashboardController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AssignedButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CancelledButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CompletedButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton NewButton { get; set; }

        [Action ("AssignedButtonTouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void AssignedButtonTouchUpInside (UIKit.UIButton sender);

        [Action ("CancelledButtonTouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void CancelledButtonTouchUpInside (UIKit.UIButton sender);

        [Action ("CompletedButtonTouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void CompletedButtonTouchUpInside (UIKit.UIButton sender);

        [Action ("NewButtonTouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void NewButtonTouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (AssignedButton != null) {
                AssignedButton.Dispose ();
                AssignedButton = null;
            }

            if (CancelledButton != null) {
                CancelledButton.Dispose ();
                CancelledButton = null;
            }

            if (CompletedButton != null) {
                CompletedButton.Dispose ();
                CompletedButton = null;
            }

            if (NewButton != null) {
                NewButton.Dispose ();
                NewButton = null;
            }
        }
    }
}