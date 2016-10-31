using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using MasterDetail.Core.ViewModel;

namespace MasterDetail
{
    [Activity(Label = "EditPersonActivity")]
    public class EditPersonActivity : ActivityBase
    {
        private Binding<string, string> _pageTitleBinding;
        private Binding<string, string> _firstNameBinding;
        private Binding<string, string> _emailBinding;
        private Binding<DateTime, string> _birthdayBinding;
        private Binding<string, string> _lastNameBinding;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddPerson);
            Vm.Init();
            
            // Bindings
            _pageTitleBinding = this.SetBinding(
                () => Vm.PageTitle,
                () => ActionBar.Title,
                BindingMode.TwoWay);

            _firstNameBinding = this.SetBinding(
                () => Vm.PersonUpdated.FirstName,
                () => FirstNameEditText.Text,
                BindingMode.TwoWay);

            _lastNameBinding = this.SetBinding(
                () => Vm.PersonUpdated.LastName,
                () => LastNameEditText.Text,
                BindingMode.TwoWay);

            _birthdayBinding = this.SetBinding(
                () => Vm.PersonUpdated.Birthday,
                () => BirthdayEditText.Text,
                BindingMode.TwoWay);

            _emailBinding = this.SetBinding(
                () => Vm.PersonUpdated.Email,
                () => EmailEditText.Text,
                BindingMode.TwoWay);
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.AddPersonMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.TitleFormatted.ToString().Equals("Cancel"))
            {
                Vm.NavigateBackCommand.Execute("");
            }
            else if (item.TitleFormatted.ToString().Equals("Save"))
            {
                Vm.SavePersonCommand.Execute("");
            }
            return base.OnOptionsItemSelected(item);
        }

        private EditPresonViewModel Vm => App.Locator.EditPersonVm;
        private Toolbar PeopleToolbar => FindViewById<Toolbar>(Resource.Id.PeopleToolbar);
        private EditText FirstNameEditText => FindViewById<EditText>(Resource.Id.EditPerson_FirstNameEditText);
        private EditText LastNameEditText => FindViewById<EditText>(Resource.Id.EditPerson_LastNameEditText);
        private EditText EmailEditText => FindViewById<EditText>(Resource.Id.EditPerson_EmailEditText);
        private EditText BirthdayEditText => FindViewById<EditText>(Resource.Id.EditPerson_BirthdayEditText);
    }
}