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
    [Register ("UserListController")]
    partial class UserListController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AdminsButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton TenantsButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton WorkersButton { get; set; }

        [Action ("TopButtonTouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TopButtonTouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (AdminsButton != null) {
                AdminsButton.Dispose ();
                AdminsButton = null;
            }

            if (TenantsButton != null) {
                TenantsButton.Dispose ();
                TenantsButton = null;
            }

            if (WorkersButton != null) {
                WorkersButton.Dispose ();
                WorkersButton = null;
            }
        }
    }
}