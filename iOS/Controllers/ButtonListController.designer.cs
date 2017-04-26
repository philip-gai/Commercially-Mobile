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
    [Register ("ButtonListController")]
    partial class ButtonListController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DiscoveredButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton IgnoredButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton PairedButton { get; set; }

        [Action ("TopButtonTouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TopButtonTouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (DiscoveredButton != null) {
                DiscoveredButton.Dispose ();
                DiscoveredButton = null;
            }

            if (IgnoredButton != null) {
                IgnoredButton.Dispose ();
                IgnoredButton = null;
            }

            if (PairedButton != null) {
                PairedButton.Dispose ();
                PairedButton = null;
            }
        }
    }
}