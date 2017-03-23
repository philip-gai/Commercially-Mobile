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
    [Register ("RequestDetailsController")]
    partial class RequestDetailsController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AcceptedTimeLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AssignButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AssignedToLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStackView ButtonStackView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CompletedTimeLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel DescriptionLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ReceivedTimeLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RoomLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SaveButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel StatusLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView StatusPickerView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStackView StatusStackView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView UrgentIndicator { get; set; }

        [Action ("AssignButtonPress:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void AssignButtonPress (UIKit.UIButton sender);

        [Action ("SaveChangesButtonPress:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SaveChangesButtonPress (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (AcceptedTimeLabel != null) {
                AcceptedTimeLabel.Dispose ();
                AcceptedTimeLabel = null;
            }

            if (AssignButton != null) {
                AssignButton.Dispose ();
                AssignButton = null;
            }

            if (AssignedToLabel != null) {
                AssignedToLabel.Dispose ();
                AssignedToLabel = null;
            }

            if (ButtonStackView != null) {
                ButtonStackView.Dispose ();
                ButtonStackView = null;
            }

            if (CompletedTimeLabel != null) {
                CompletedTimeLabel.Dispose ();
                CompletedTimeLabel = null;
            }

            if (DescriptionLabel != null) {
                DescriptionLabel.Dispose ();
                DescriptionLabel = null;
            }

            if (ReceivedTimeLabel != null) {
                ReceivedTimeLabel.Dispose ();
                ReceivedTimeLabel = null;
            }

            if (RoomLabel != null) {
                RoomLabel.Dispose ();
                RoomLabel = null;
            }

            if (SaveButton != null) {
                SaveButton.Dispose ();
                SaveButton = null;
            }

            if (StatusLabel != null) {
                StatusLabel.Dispose ();
                StatusLabel = null;
            }

            if (StatusPickerView != null) {
                StatusPickerView.Dispose ();
                StatusPickerView = null;
            }

            if (StatusStackView != null) {
                StatusStackView.Dispose ();
                StatusStackView = null;
            }

            if (UrgentIndicator != null) {
                UrgentIndicator.Dispose ();
                UrgentIndicator = null;
            }
        }
    }
}