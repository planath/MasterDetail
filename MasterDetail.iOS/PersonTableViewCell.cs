using System;

using Foundation;
using UIKit;

namespace MasterDetail.iOS
{
    public partial class PersonTableViewCell : UITableViewCell
    {
        protected PersonTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public PersonTableViewCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            // Note: this .ctor should not contain any initialization logic.
            SelectionStyle = UITableViewCellSelectionStyle.Gray;
        }

        public string Name { get { return persName.Text; } set { persName.Text = value; } }
        public string Email { get { return persEmail.Text; } set { persEmail.Text = value; } }

        public string Image
        {
            set
            {
                persImageView.Image = UIImage.FromBundle(value);
                var width = persImageView.Frame.Width;
                persImageView.Layer.CornerRadius = width/2;
                persImageView.ClipsToBounds = true;

                bgImage.Image = UIImage.FromBundle(value);
            }
        }
    }
}

