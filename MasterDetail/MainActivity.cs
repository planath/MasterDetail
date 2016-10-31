using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using MasterDetail.Core.Model;
using MasterDetail.Core.ViewModel.mvvmlight.Core.ViewModel;

namespace MasterDetail
{
    //[Activity(Label = "MasterDetail")]
    [Activity(Label = "Personen", MainLauncher = true, Theme = "@style/AppTheme", Icon = "@drawable/icon")]
    public class MainActivity : ActivityBase
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            SetActionBar(PeopleToolbar);
            ActionBar.Title = "Kontakte";

            Vm.Init();
            PeopleListView.Adapter = Vm.People.GetAdapter(GetPersonView);

            PeopleListView.ItemClick += ListView_ItemClick;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.PeopleMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.TitleFormatted.ToString().Equals("Add"))
            {
                Vm.AddPersonCommand.Execute("");
            }
            return base.OnOptionsItemSelected(item);
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = PeopleListView.Adapter.GetItem(e.Position);

            var propertyInfo = item.GetType().GetProperty("Instance");
            var person = propertyInfo == null ? null : propertyInfo.GetValue(item, null) as Person;
            if (person != null)
            {
                Vm.SelectedPerson = person;
            }
        }

        private View GetPersonView(int position, Person person, View convertView)
        {
            View view = convertView ?? LayoutInflater.Inflate(Resource.Layout.RowItem, null);

            var name = view.FindViewById<TextView>(Resource.Id.Name);
            var email = view.FindViewById<TextView>(Resource.Id.Email);

            name.Text = person.Name;
            email.Text = person.Email;

            return view;
        }

        private PeopleViewModel Vm => App.Locator.MainVm;
        private ListView PeopleListView => FindViewById<ListView>(Resource.Id.PeopleListView);
        private Toolbar PeopleToolbar => FindViewById<Toolbar>(Resource.Id.PeopleToolbar);
    }
}

