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
    [Register ("MasterViewController")]
    partial class MasterViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView FirstPage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (FirstPage != null) {
                FirstPage.Dispose ();
                FirstPage = null;
            }
        }
    }
}