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
    [Register ("PromptController")]
    partial class PromptController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BackgroundButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DismissButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel PromptLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BackgroundButton != null) {
                BackgroundButton.Dispose ();
                BackgroundButton = null;
            }

            if (DismissButton != null) {
                DismissButton.Dispose ();
                DismissButton = null;
            }

            if (PromptLabel != null) {
                PromptLabel.Dispose ();
                PromptLabel = null;
            }
        }
    }
}