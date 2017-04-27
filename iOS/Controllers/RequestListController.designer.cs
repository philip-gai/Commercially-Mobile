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
    [Register ("RequestListController")]
    partial class RequestListController
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

        [Action ("TopButtonTouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TopButtonTouchUpInside (UIKit.UIButton sender);

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