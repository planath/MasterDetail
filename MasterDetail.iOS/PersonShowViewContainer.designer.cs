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
    [Register ("PersonShowViewContainer")]
    partial class PersonShowViewContainer
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel persShowBirthday { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel persShowEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel persShowFirstname { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel persShowLastname { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (persShowBirthday != null) {
                persShowBirthday.Dispose ();
                persShowBirthday = null;
            }

            if (persShowEmail != null) {
                persShowEmail.Dispose ();
                persShowEmail = null;
            }

            if (persShowFirstname != null) {
                persShowFirstname.Dispose ();
                persShowFirstname = null;
            }

            if (persShowLastname != null) {
                persShowLastname.Dispose ();
                persShowLastname = null;
            }
        }
    }
}