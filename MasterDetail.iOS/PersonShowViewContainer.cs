using System;
using GalaSoft.MvvmLight.Helpers;
using MasterDetail.Core.ViewModel;
using UIKit;

namespace MasterDetail.iOS
{
    public partial class PersonShowViewContainer : UITableViewController
    {
        private Binding<string, string> _firstNameLabelBinding;
        private Binding<string, string> _lastNameLabelBinding;
        private Binding<string, string> _birthdayLabelBinding;
        private Binding<string, string> _emailLabelBinding;

        public PersonShowViewContainer(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _firstNameLabelBinding = this.SetBinding(
                () => Vm.CurrentPerson.FirstName,
                () => persShowFirstname.Text);

            _lastNameLabelBinding = this.SetBinding(
                () => Vm.CurrentPerson.LastName,
                () => persShowLastname.Text);

            _birthdayLabelBinding = this.SetBinding(
                () => Vm.BirthdayString,
                () => persShowBirthday.Text);

            _emailLabelBinding = this.SetBinding(
                () => Vm.CurrentPerson.Email,
                () => persShowEmail.Text);
        }

        private PersonViewModel Vm => Application.Locator.DetailVm;
    }
}