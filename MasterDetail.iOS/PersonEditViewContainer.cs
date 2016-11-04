using System;
using GalaSoft.MvvmLight.Helpers;
using MasterDetail.Core.ViewModel;
using UIKit;

namespace MasterDetail.iOS
{
    public partial class PersonEditViewContainer : UITableViewController
    {
        private Binding<string, string> _firstNameTextBinding;
        private Binding<string, string> _lastNameTextBinding;
        private Binding<string, string> _birthdayTextBinding;
        private Binding<string, string> _emailTextBinding;
        private Binding<string, string> _lastNameTextEditedBinding;
        private Binding<string, string> _firstNameTextEditedBinding;
        private Binding<string, string> _emailTextEditedBinding;
        private Binding<string, string> _birthdayTextEditedBinding;

        public PersonEditViewContainer(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            // text bindings from vm to view
            _firstNameTextBinding = this.SetBinding(
                () => Vm.CurrentPerson.FirstName,
                () => persEditFirstname.Text);

            _lastNameTextBinding = this.SetBinding(
                () => Vm.CurrentPerson.LastName,
                () => persEditLastname.Text);

            _birthdayTextBinding = this.SetBinding(
                () => Vm.BirthdayString,
                () => persEditBirthday.Text);

            _emailTextBinding = this.SetBinding(
                () => Vm.CurrentPerson.Email,
                () => persEditEmail.Text);

            // event binding if update from view to vm
            _firstNameTextEditedBinding = this.SetBinding(
                () => persEditFirstname.Text)
                .ObserveSourceEvent("EditingChanged")
                .WhenSourceChanges(() => Vm.CurrentPerson.FirstName = persEditFirstname.Text);

            _lastNameTextEditedBinding = this.SetBinding(
                () => persEditLastname.Text)
                .ObserveSourceEvent("EditingChanged")
                .WhenSourceChanges(() => Vm.CurrentPerson.LastName = persEditLastname.Text);

            _emailTextEditedBinding = this.SetBinding(
                () => persEditEmail.Text)
                .ObserveSourceEvent("EditingChanged")
                .WhenSourceChanges(() => Vm.CurrentPerson.Email = persEditEmail.Text);

            _birthdayTextEditedBinding = this.SetBinding(
                () => persEditBirthday.Text)
                .ObserveSourceEvent("EditingChanged")
                .WhenSourceChanges(() => Vm.BirthdayString = persEditBirthday.Text);
        }


        private PersonViewModel Vm => Application.Locator.DetailVm;
    }
}