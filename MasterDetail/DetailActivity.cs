using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using MasterDetail.Core.ViewModel;
using MasterDetail.Resources.CustomView;

namespace MasterDetail.Resources.layout
{
    //[Activity(Label = "Person", MainLauncher = true, Icon = "@drawable/icon")]
    [Activity(Label = "Person")]
    public class DetailActivity : ActivityBase
    {
        private Binding<string, string> _personNameBinding;
        private Binding<string, string> _personEmailBinding;
        private Binding<string, string> _personDateBinding;
        private Binding<string, string> _personDateEditBinding;
        private Binding<string, string> _personEmailEditBinding;
        private Binding<string, string> _personFirstnameEditBinding;
        private Binding<string, string> _personLastnameEditBinding;
        private IMenu _menu;
        private bool _editMode = false;
        private PersonEditFragment _fragmentEdit;
        private PersonShowFragment _fragmentShow;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Detail);
            // create Fragments
            _fragmentShow = new PersonShowFragment();
            _fragmentEdit = new PersonEditFragment();
            var fragmentTransaction = FragmentManager.BeginTransaction();
            fragmentTransaction.Add(Resource.Id.FragmentContainerPersonDetail, _fragmentShow, "showDetailFragment");
            fragmentTransaction.Add(Resource.Id.FragmentContainerPersonDetail, _fragmentEdit, "editDetailFragment");
            fragmentTransaction.Commit();
        }
        
        protected override void OnStart()
        {
            base.OnStart();
            Vm.Init();

            // Bindings
            SetBindings();

            // Hide unwanted Fragment after successful binding
            OnlyDisplayWantedFragment();
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.PersonMenu, menu);
            _menu = menu;
            DisplayMenuItemsAccordingToEditMode(_menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.TitleFormatted.ToString().Equals("Save"))
            {
                _editMode = !_editMode;
                OnlyDisplayWantedFragment();
                Vm.SavePersonCommand.Execute("");
            }
            else if (item.TitleFormatted.ToString().Equals("Edit"))
            {
                _editMode = !_editMode;
                OnlyDisplayWantedFragment();
            }
            else if (item.TitleFormatted.ToString().Equals("Delete"))
            {
                Vm.RemovePersonCommand.Execute("");
            }

            return base.OnOptionsItemSelected(item);
        }

        private void OnlyDisplayWantedFragment()
        {
            DetermineIfNewPersonShouldBeCreated();
            var fragmentTransaction = FragmentManager.BeginTransaction();
            if (!_editMode)
            {
                fragmentTransaction.Show(_fragmentShow);
                fragmentTransaction.Hide(_fragmentEdit);
                fragmentTransaction.Commit();
            }
            else
            {
                fragmentTransaction.Show(_fragmentEdit);
                fragmentTransaction.Hide(_fragmentShow);
                fragmentTransaction.Commit();
            }
            if (_menu != null)
            {
                DisplayMenuItemsAccordingToEditMode(_menu);
            }
        }

        private void DetermineIfNewPersonShouldBeCreated()
        {
            if (string.IsNullOrEmpty(Vm.CurrentPerson.FirstName))
            {
                _editMode = true;
            }
        }

        private void DisplayMenuItemsAccordingToEditMode(IMenu menu)
        {
            menu.FindItem(Resource.Id.PersonMenuEdit).SetVisible(!_editMode);
            menu.FindItem(Resource.Id.PersonMenuSave).SetVisible(_editMode);
        }

        private void SetBindings()
        {
            //bindings for PersonHeader
            PersonHeader.Title = Vm.CurrentPerson.Name;

            //bindings for showFragment
            _personNameBinding = this.SetBinding(
                () => Vm.CurrentPerson.Name,
                () => NameTextView.Text,
                BindingMode.TwoWay);

            _personEmailBinding = this.SetBinding(
                () => Vm.CurrentPerson.Email,
                () => EmailTextView.Text,
                BindingMode.TwoWay);

            _personDateBinding = this.SetBinding(
                () => Vm.BirthdayString,
                () => BirthdayTextView.Text,
                BindingMode.TwoWay);

            //bindings for editFragment
            _personFirstnameEditBinding = this.SetBinding(
                () => Vm.CurrentPerson.FirstName,
                () => FirstnameEditText.Text,
                BindingMode.TwoWay);

            _personLastnameEditBinding = this.SetBinding(
                () => Vm.CurrentPerson.LastName,
                () => LastnameEditText.Text,
                BindingMode.TwoWay);

            _personEmailEditBinding = this.SetBinding(
                () => Vm.CurrentPerson.Email,
                () => EmailEditText.Text,
                BindingMode.TwoWay);

            _personDateEditBinding = this.SetBinding(
                () => Vm.BirthdayString,
                () => BirthdayEditText.Text,
                BindingMode.TwoWay);
        }

        private PersonViewModel Vm => App.Locator.DetailVm;
        private PersonHeader PersonHeader => FindViewById<PersonHeader>(Resource.Id.PersonHeader);
        private TextView BirthdayTextView => FindViewById<TextView>(Resource.Id.BirthdayTextView);
        private TextView EmailTextView => FindViewById<TextView>(Resource.Id.EmailTextView);
        private TextView NameTextView => FindViewById<TextView>(Resource.Id.NameTextView);
        private EditText BirthdayEditText => FindViewById<EditText>(Resource.Id.BirthdayEditFragment);
        private EditText EmailEditText => FindViewById<EditText>(Resource.Id.EmailEditFragment);
        private EditText FirstnameEditText => FindViewById<EditText>(Resource.Id.FirstameEditFragment);
        private EditText LastnameEditText => FindViewById<EditText>(Resource.Id.LastnameEditFragment);
    }
}