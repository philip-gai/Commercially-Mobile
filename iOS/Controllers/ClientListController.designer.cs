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
    [Register ("ClientListController")]
    partial class ClientListController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AuthorizedButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DiscoveredButton { get; set; }

        [Action ("TopButtonTouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TopButtonTouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (AuthorizedButton != null) {
                AuthorizedButton.Dispose ();
                AuthorizedButton = null;
            }

            if (DiscoveredButton != null) {
                DiscoveredButton.Dispose ();
                DiscoveredButton = null;
            }
        }
    }
}