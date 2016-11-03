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
    [Register ("PersonEditViewContainer")]
    partial class PersonEditViewContainer
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField persEditBirthday { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField persEditEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField persEditFirstname { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField persEditLastname { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (persEditBirthday != null) {
                persEditBirthday.Dispose ();
                persEditBirthday = null;
            }

            if (persEditEmail != null) {
                persEditEmail.Dispose ();
                persEditEmail = null;
            }

            if (persEditFirstname != null) {
                persEditFirstname.Dispose ();
                persEditFirstname = null;
            }

            if (persEditLastname != null) {
                persEditLastname.Dispose ();
                persEditLastname = null;
            }
        }
    }
}