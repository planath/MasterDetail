using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using MasterDetail.Core.Model;
using MasterDetail.Core.ViewModel;

namespace MasterDetail.Resources.layout
{
    //[Activity(Label = "Person", MainLauncher = true, Icon = "@drawable/icon")]
    [Activity(Label = "Person")]
    public class DetailActivity : ActivityBase
    {
        private Person _person;
        private Binding<string, string> _personNameBinding;
        private Binding<string, string> _personEmailBinding;
        private Binding<string, string> _personDateBinding;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Detail);
            
            Vm.Init();

            // Bindings
            _personNameBinding = this.SetBinding(
                () => Vm.CurrentPerson.Name,
                () => NameEditText.Text,
                BindingMode.TwoWay);

            _personEmailBinding = this.SetBinding(
                () => Vm.CurrentPerson.Email,
                () => EmailEditText.Text,
                BindingMode.TwoWay);

            _personDateBinding = this.SetBinding(
                () => Vm.BirthdayString,
                () => BirthdayEditText.Text,
                BindingMode.TwoWay);

            //BackButton.SetCommand(Vm.NavigateBackCommand);
            //EditButton.SetCommand(Vm.EditPersonCommand);
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.PersonMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.TitleFormatted.ToString().Equals("Save"))
            {
                Vm.SavePersonCommand.Execute("");
            }
            else if (item.TitleFormatted.ToString().Equals("Delete"))
            {
                Vm.RemovePersonCommand.Execute("");
            }
            return base.OnOptionsItemSelected(item);
        }

        private bool EditMode { get; set; }
        private PersonViewModel Vm => App.Locator.DetailVm;
        private EditText BirthdayEditText => FindViewById<EditText>(Resource.Id.BirthdayEditText);
        private EditText EmailEditText => FindViewById<EditText>(Resource.Id.EmailEditText);
        private EditText NameEditText => FindViewById<EditText>(Resource.Id.NameEditText);
    }
}