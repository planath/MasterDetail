using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using MasterDetail.Adapter;
using MasterDetail.Core.Model;
using MasterDetail.Core.ViewModel.mvvmlight.Core.ViewModel;

namespace MasterDetail
{
    //[Activity(Label = "CardsActivity")]
    [Activity(Label = "CardsActivity", MainLauncher = true, Theme = "@style/AppTheme", Icon = "@drawable/icon")]
    public class CardsActivity : Activity
    {
        protected CardsActivity(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Init();
        }

        public CardsActivity()
        {
            Init();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CardRecycler);
            SetUpRecycleView();
        }

        private void Init()
        {
            Vm.Init();
        }
        private void SetUpRecycleView()
        {
            RecyclerView myRecyclerView = (RecyclerView)FindViewById(Resource.Id.RecyclerView);
            var adapter = new CardRecyclerAdapter(this, Vm.People);
            myRecyclerView.SetAdapter(adapter);

            var linearLayoutVerticalManager = new LinearLayoutManager(this);
            linearLayoutVerticalManager.Orientation = LinearLayoutManager.Vertical;
            myRecyclerView.SetLayoutManager(linearLayoutVerticalManager);

            myRecyclerView.SetItemAnimator(new DefaultItemAnimator());
        }
        private PeopleViewModel Vm => App.Locator.MainVm;
        private RecyclerView PeopleListView => FindViewById<RecyclerView>(Resource.Id.RecyclerView);
    }
}
