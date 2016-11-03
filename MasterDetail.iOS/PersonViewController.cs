using Foundation;
using System;
using GalaSoft.MvvmLight.Helpers;
using MasterDetail.Core.ViewModel;
using UIKit;

namespace MasterDetail.iOS
{
    public partial class PersonViewController : UIViewController
    {
        private bool editMode = false;
        private Binding<string, string> _nameLabelBinding;

        public PersonViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _nameLabelBinding = this.SetBinding(
                () => Vm.CurrentPerson.Name,
                () => persName.Text);
        }

        partial void EditToggleButton_TouchUpInside(UIButton sender)
        {
            if (editMode)
            {
                ContainerEdit.Hidden = false;
                ContainerShow.Hidden = true;
            }
            else
            {
                ContainerShow.Hidden = false;
                ContainerEdit.Hidden = true;
            }
            editMode = !editMode;
        }
        private PersonViewModel Vm => Application.Locator.DetailVm;
    }
}