using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Messaging;
using MasterDetail.Core.Model;
using MasterDetail.Core.ViewModel.Helper;
using MasterDetail.Core.ViewModel.mvvmlight.Core.ViewModel;
using MasterDetail.Resources.layout;

namespace MasterDetail.Adapter
{
    public class CardRecyclerAdapter : RecyclerView.Adapter
    {
        private readonly ObservableCollection<Person> _data;
        private LayoutInflater _inflater;
        private int _selectedPosition;

        public CardRecyclerAdapter(Activity context, ObservableCollection<Person> data)
        {
            _inflater = LayoutInflater.From(context);
            _data = data;

            Vm.Init();
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            holder.ItemView.Selected = _selectedPosition == position;
            if (position <= _data.Count)
            {
                var currentObject = _data[position];
                var element = holder as PersonCardViewHolder;
                element.SetData(currentObject, position);

                element.EditButton.Click += (object sender, EventArgs e) =>
                {
                    var person = currentObject;
                    var msg = new GoToPageMessage {PersonId = person.Id };
                    Messenger.Default.Send<GoToPageMessage>(msg);
                };
                element.DeleteButton.Click += (object sender, EventArgs e) =>
                {
                    var person = currentObject;
                    var msg = new GoToPageMessage { PersonId = person.Id };
                    Messenger.Default.Send<GoToPageMessage>(msg);
                };
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = _inflater.Inflate(Resource.Layout.PersonCardView, parent, false);
            var viewHolder = new PersonCardViewHolder(view);
            return viewHolder;
        }

        public override int ItemCount { get { return _data.Count; } }
        private PeopleViewModel Vm => App.Locator.MainVm;
    }
}