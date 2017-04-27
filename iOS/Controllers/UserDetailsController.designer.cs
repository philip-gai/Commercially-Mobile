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
        UIKit.UILabel EmailLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel FirstLastLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel PhoneLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (EmailLabel != null) {
                EmailLabel.Dispose ();
                EmailLabel = null;
            }

            if (FirstLastLabel != null) {
                FirstLastLabel.Dispose ();
                FirstLastLabel = null;
            }

            if (PhoneLabel != null) {
                PhoneLabel.Dispose ();
                PhoneLabel = null;
            }
        }
    }
}