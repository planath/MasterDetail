using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace MasterDetail.Resources.CustomView
{
    public class RoundedCornerLayout : FrameLayout
    {
        private static readonly float CORNER_RADIUS = 40.0f;
        protected RoundedCornerLayout(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Init();
        }

        public RoundedCornerLayout(Context context) : base(context)
        {
            Init(context);
        }

        public RoundedCornerLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context);
        }

        public RoundedCornerLayout(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context);
        }

        public RoundedCornerLayout(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(context);
        }

        private void Init(Context context = null)
        {
            //if (context != null)
            //{
            //    DisplayMetrics metrics = context.getResources().getDisplayMetrics();
            //    var cornerRadius = TypedValue.ApplyDimension(TypedValue.comp, CORNER_RADIUS, metrics);

            //    paint = new Paint(Paint.ANTI_ALIAS_FLAG);

            //    maskPaint = new Paint(Paint.ANTI_ALIAS_FLAG | Paint.FILTER_BITMAP_FLAG);
            //    maskPaint.setXfermode(new PorterDuffXfermode(PorterDuff.Mode.CLEAR));

            //    setWillNotDraw(false);
            //}
        }
    }
}