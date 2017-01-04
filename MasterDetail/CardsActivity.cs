using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using MasterDetail.Adapter;
using MasterDetail.Core.Model;
using MasterDetail.Core.ViewModel.mvvmlight.Core.ViewModel;

namespace MasterDetail
{
    //[Activity(Label = "CardsActivity")]
    [Activity(Label = "Kontakte", MainLauncher = true, Theme = "@style/AppTheme", Icon = "@drawable/icon")]
    public class CardsActivity : ActivityBase
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Vm.Init();
            SetContentView(Resource.Layout.CardRecycler);
            SetUpRecycleView();
            SetActionBar(MainToolbar);
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

        private void SetUpRecycleView()
        {
            var adapter = new CardRecyclerAdapter(this, Vm.People);
            PeopleListView.SetAdapter(adapter);

            var linearLayoutVerticalManager = new LinearLayoutManager(this);
            linearLayoutVerticalManager.Orientation = LinearLayoutManager.Vertical;
            PeopleListView.SetLayoutManager(linearLayoutVerticalManager);

            PeopleListView.SetItemAnimator(new DefaultItemAnimator());
        }
        private PeopleViewModel Vm => App.Locator.MainVm;
        private Toolbar MainToolbar => FindViewById<Toolbar>(Resource.Id.MainToolbar);
        private RecyclerView PeopleListView => FindViewById<RecyclerView>(Resource.Id.RecyclerView);
    }
}
