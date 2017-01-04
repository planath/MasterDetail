using System;

using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace MasterDetail.Resources.CustomView
{
    public class PersonHeader : FrameLayout
    {
        private BlurredImage _background;
        private ImageView _thumbnail;
        private TextView _title;

        #region Constructors
        public PersonHeader(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Init();
        }

        public PersonHeader(Context context) : base(context)
        {
            Init(context);
        }

        public PersonHeader(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public PersonHeader(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        public PersonHeader(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(context, attrs);
        }
        #endregion

        public string Title { get { return _title.Text; } set { _title.Text = value; } }
        private void Init(Context context = null, IAttributeSet attrs = null)
        {
            var backgroundSrc = Resource.Drawable.journey7;
            var thumbnailSrc = Resource.Drawable.journey7;
            var title = string.Empty;

            if (attrs != null && context != null)
            {
                TypedArray array = context.ObtainStyledAttributes(attrs, Resource.Styleable.PersonHeader);
                backgroundSrc = array.GetResourceId(Resource.Styleable.PersonHeader_backgroundSrc, backgroundSrc);
                thumbnailSrc = array.GetResourceId(Resource.Styleable.PersonHeader_thumbnailSrc, thumbnailSrc);
                title = array.GetString(Resource.Styleable.PersonHeader_title);
            }


            //////////////////////////////////////////////
            if (context != null)
            {
                Inflate(context, Resource.Layout.PersonHeader, this);
                _background = FindViewById<BlurredImage>(Resource.Id.PersonHeaderBackgorund);
                _thumbnail = FindViewById<ImageView>(Resource.Id.PersonHeaderThumbnail);
                _title = FindViewById<TextView>(Resource.Id.PersonHeaderTitle);

                _background.SetImageResource(backgroundSrc);
                _thumbnail.SetImageResource(thumbnailSrc);
                _thumbnail.ClipToOutline = true;
                _title.Text = title;
            }
        }

   
    }
}