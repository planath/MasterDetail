using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using MasterDetail.Core.ViewModel;

namespace MasterDetail
{
    //[Activity(Label = "Personen", MainLauncher = true, Theme = "@style/AppTheme", Icon = "@drawable/icon")]
    [Activity(Label = "Neuen Kontakt", Theme = "@style/AppTheme")]
    public class AddPersonActivity : ActivityBase
    {
        private Binding<string, string> _pageTitleBinding;
        private Binding<string, string> _firstNameBinding;
        private Binding<string, string> _lastNameBinding;
        private Binding<string, string> _emailBinding;
        private Binding<string, string> _birthdayBinding;
        private PersonShowFragment _fragment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddPerson);
            //SetActionBar(PeopleToolbar);


            // create Fragments
            _fragment = new PersonShowFragment();
            var fragmentTransaction = FragmentManager.BeginTransaction();
            fragmentTransaction.Add(Resource.Id.FragmentContainerPersonDetail, _fragment, "showDetailFragment");
            fragmentTransaction.Commit();


            Vm.Init();
            
            // Bindings
            //_pageTitleBinding = this.SetBinding(
            //    () => Vm.PageTitle,
            //    () => ActionBar.Title,
            //    BindingMode.TwoWay);

            //_firstNameBinding = this.SetBinding(
            //    () => Vm.FirstName,
            //    () => FirstNameEditText.Text,
            //    BindingMode.TwoWay);

            //_lastNameBinding = this.SetBinding(
            //    () => Vm.LastName,
            //    () => LastNameEditText.Text,
            //    BindingMode.TwoWay);

            //_birthdayBinding = this.SetBinding(
            //    () => Vm.Birthday,
            //    () => BirthdayEditText.Text,
            //    BindingMode.TwoWay);

            //_emailBinding = this.SetBinding(
            //    () => Vm.Email,
            //    () => EmailEditText.Text,
            //    BindingMode.TwoWay);
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.AddPersonMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.TitleFormatted.ToString().Equals("Close"))
            {
                Vm.NavigateBackCommand.Execute("");
            }
            else if (item.TitleFormatted.ToString().Equals("Save"))
            {
                Vm.SavePersonCommand.Execute("");
            }
            return base.OnOptionsItemSelected(item);
        }

        private AddPersonViewModel Vm => App.Locator.AddPersonVm;
        private Toolbar PeopleToolbar => FindViewById<Toolbar>(Resource.Id.PeopleToolbar);
        //private EditText FirstNameEditText => FindViewById<EditText>(Resource.Id.AddPerson_FirstNameEditText);
        //private EditText LastNameEditText => FindViewById<EditText>(Resource.Id.AddPerson_LastNameEditText);
        //private EditText EmailEditText => FindViewById<EditText>(Resource.Id.AddPerson_EmailEditText);
        //private EditText BirthdayEditText => FindViewById<EditText>(Resource.Id.AddPerson_BirthdayEditText);
    }
}