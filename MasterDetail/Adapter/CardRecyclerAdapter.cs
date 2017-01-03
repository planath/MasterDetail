using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MasterDetail.Core.Model;
using MasterDetail.Core.ViewModel.mvvmlight.Core.ViewModel;
using MasterDetail.Resources.layout;

namespace MasterDetail.Adapter
{
    public class CardRecyclerAdapter : RecyclerView.Adapter
    {
        private readonly ObservableCollection<Person> _data;
        private LayoutInflater _inflater;
        private int _position;

        public CardRecyclerAdapter(Activity context, ObservableCollection<Person> data)
        {
            _inflater = LayoutInflater.From(context);
            _data = data;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            _position = position;
            if (position <= _data.Count)
            {
                var currentObject = _data[position];
                var element = holder as PersonCardViewHolder;
                element.SetData(currentObject, position);

                element.EditButton.Click += EditButton_Click;
                element.DeleteButton.Click += DeleteButton_Click;
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            // show detail page
            if (_position <= _data.Count)
            {
                var person = _data[_position];
                Vm.SelectedPerson = person;
            }
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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