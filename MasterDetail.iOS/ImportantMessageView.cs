using Foundation;
using System;
using ObjCRuntime;
using UIKit;

namespace MasterDetail.iOS
{
    public partial class ImportantMessageView : UIView
    {
        public ImportantMessageView (IntPtr handle) : base (handle)
        {
        }
        public static ImportantMessageView Create()
        {

            var arr = NSBundle.MainBundle.LoadNib("ImportantMessageView", null, null);
            var v = Runtime.GetNSObject<ImportantMessageView>(arr.ValueAt(0));

            return v;
        }

        public override void AwakeFromNib()
        {

            MessageLabel.Text = "No Internet";
        }
    }
}