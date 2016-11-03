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

namespace MasterDetail.iOS
{
    [Register ("PersonTableViewCell")]
    partial class PersonTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView bgImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel persEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView persImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel persName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel PersonCellDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel PersonCellEmailLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView PersonCellImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel PersonCellNameLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (bgImage != null) {
                bgImage.Dispose ();
                bgImage = null;
            }

            if (persEmail != null) {
                persEmail.Dispose ();
                persEmail = null;
            }

            if (persImageView != null) {
                persImageView.Dispose ();
                persImageView = null;
            }

            if (persName != null) {
                persName.Dispose ();
                persName = null;
            }

            if (PersonCellDescription != null) {
                PersonCellDescription.Dispose ();
                PersonCellDescription = null;
            }

            if (PersonCellEmailLabel != null) {
                PersonCellEmailLabel.Dispose ();
                PersonCellEmailLabel = null;
            }

            if (PersonCellImageView != null) {
                PersonCellImageView.Dispose ();
                PersonCellImageView = null;
            }

            if (PersonCellNameLabel != null) {
                PersonCellNameLabel.Dispose ();
                PersonCellNameLabel = null;
            }
        }
    }
}