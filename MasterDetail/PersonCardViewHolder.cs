using System;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MasterDetail.Core.Model;

namespace MasterDetail
{
    public class PersonCardViewHolder : RecyclerView.ViewHolder
    {
        private int _position;
        private Person _current;
        private ImageView _backgournd;
        private TextView _name;
        private TextView _firstname;
        private TextView _lastname;
        private TextView _email;
        private ImageView _image;

        public PersonCardViewHolder(View itemView) : base(itemView)
        {
            _backgournd = itemView.FindViewById<ImageView>(Resource.Id.PersonCardBackground);
            _image = itemView.FindViewById<ImageView>(Resource.Id.PersonCardImage);
            _name = itemView.FindViewById<TextView>(Resource.Id.PersonCardName);
            _firstname = itemView.FindViewById<TextView>(Resource.Id.PersonCardFirstName);
            _lastname = itemView.FindViewById<TextView>(Resource.Id.PersonCardLastName);
            _email = itemView.FindViewById<TextView>(Resource.Id.PersonCardEmail);
            DeleteButton = itemView.FindViewById<ImageButton>(Resource.Id.PersonCardDelete);
            EditButton = itemView.FindViewById<ImageButton>(Resource.Id.PersonCardEdit);

        }
        public ImageButton EditButton { get; }

        public ImageButton DeleteButton { get; }

        public void SetData(Person currentObject, int position)
        {
            _current = currentObject;
            _position = position;

            _name.Text = currentObject.Name;
            _firstname.Text = currentObject.FirstName;
            _lastname.Text = currentObject.LastName;
            _email.Text = currentObject.Email;
            _backgournd.SetImageResource(RandomImage(position));
            _image.SetImageResource(RandomImage(position));
        }

        private int RandomImage(int position)
        {
            switch (position%10)
            {
                case 0:
                    return Resource.Drawable.journey10;
                case 1:
                    return Resource.Drawable.journey1;
                case 2:
                    return Resource.Drawable.journey2;
                case 3:
                    return Resource.Drawable.journey3;
                case 4:
                    return Resource.Drawable.journey4;
                case 5:
                    return Resource.Drawable.journey5;
                case 6:
                    return Resource.Drawable.journey6;
                case 7:
                    return Resource.Drawable.journey7;
                case 8:
                    return Resource.Drawable.journey8;
                case 9:
                    return Resource.Drawable.journey9;
                default:
                    return Resource.Drawable.journey0;
            }
        }
    }
}