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
    [Register ("PersonViewController")]
    partial class PersonViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ContainerEdit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView ContainerShow { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton EditToggleButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView persImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel persName { get; set; }

        [Action ("EditToggleButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void EditToggleButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (ContainerEdit != null) {
                ContainerEdit.Dispose ();
                ContainerEdit = null;
            }

            if (ContainerShow != null) {
                ContainerShow.Dispose ();
                ContainerShow = null;
            }

            if (EditToggleButton != null) {
                EditToggleButton.Dispose ();
                EditToggleButton = null;
            }

            if (persImageView != null) {
                persImageView.Dispose ();
                persImageView = null;
            }

            if (persName != null) {
                persName.Dispose ();
                persName = null;
            }
        }
    }
}