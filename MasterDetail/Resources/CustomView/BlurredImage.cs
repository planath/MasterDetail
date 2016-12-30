using System;

using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Renderscripts;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace MasterDetail.Resources.CustomView
{
    public class BlurredImage : ImageView
    {
        private Context _context;
        private IAttributeSet _attrs;

        #region Constructors

        public BlurredImage(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Init();
        }

        public BlurredImage(Context context) : base(context)
        {
            Init(context);
        }

        public BlurredImage(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        public BlurredImage(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        public BlurredImage(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
            : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(context, attrs);
        }

        #endregion

        private void Init(Context context = null, IAttributeSet attrs = null)
        {
            _context = context;
            if (attrs != null)
            {
                int imageResource = attrs.GetAttributeResourceValue("http://schemas.android.com/apk/res/android", "src", 0);
                ApplyBlur(imageResource);
            }
        }
        
        public void ApplyBlur(int imageResource)
        {
            if (_context != null)
            {
                var bitmap = GetDrawable(_context.Resources, imageResource);
                var blurredImage = BlurRenderScript(_context, bitmap, 25);
                var strongerBlurredImage = BlurRenderScript(_context, blurredImage, 25);
                var lightenedImage = LightenBitmap(strongerBlurredImage);
                var strongererBlurredImage = BlurRenderScript(_context, lightenedImage, 25);

                SetImageBitmap(strongererBlurredImage);
            }
        }
        
        /// <summary>
        /// transforms a bitmap and returns it blurred
        /// </summary>
        /// <param name="context">application context</param>
        /// <param name="smallBitmap">bitmap without blur</param>
        /// <param name="radius">blur radius must be 0 < r <= 25</param>
        /// <returns></returns>
        private Bitmap BlurRenderScript(Context context, Bitmap smallBitmap, int radius)
        {
            radius = Math.Min(radius, 25);
            radius = Math.Max(radius, 1);

            smallBitmap = RgbToArgb(smallBitmap);
            var bitmap = Bitmap.CreateBitmap(smallBitmap.Width, smallBitmap.Height, Bitmap.Config.Argb8888);

            var renderScript = RenderScript.Create(context);
            var blurInput = Allocation.CreateFromBitmap(renderScript, smallBitmap);
            var blurOutput = Allocation.CreateFromBitmap(renderScript, bitmap);

            var blur = ScriptIntrinsicBlur.Create(renderScript, Element.U8_4(renderScript));
            blur.SetInput(blurInput);
            blur.SetRadius(radius);
            blur.ForEach(blurOutput);

            blurOutput.CopyTo(bitmap);
            renderScript.Destroy();

            return bitmap;

        }
        private Bitmap LightenBitmap(Bitmap bitmap)
        {

            Canvas canvas = new Canvas(bitmap);
            Paint p = new Paint();
            ColorFilter filter = new LightingColorFilter(unchecked((int)0xFFFFFFFF), 0x00333333); // lighten
            p.Color = Color.White;
            p.SetColorFilter(filter);
            canvas.DrawBitmap(bitmap, new Matrix(), p);

            return bitmap;
        }

        private Bitmap RgbToArgb(Bitmap image)
        {
            var numberOfPixels = image.Width * image.Height;
            var pixels = new int[numberOfPixels];

            //Get JPEG pixels. Each int is the color values for one pixel. Copy it into first passed parameter.
            image.GetPixels(pixels, 0, image.Width, 0, 0, image.Width, image.Height);
            //Create a Bitmap of the appropriate format.
            var resultingImage = Bitmap.CreateBitmap(image.Width, image.Height, Bitmap.Config.Argb8888);
            //Set RGB pixels.
            resultingImage.SetPixels(pixels, 0, resultingImage.Width, 0, 0, resultingImage.Width, resultingImage.Height);

            return resultingImage;
        }

        public override void SetImageResource(int resId)
        {
            base.SetImageResource(resId);
            ApplyBlur(resId);
        }

        public static Bitmap GetDrawable(Android.Content.Res.Resources res, int id)
        {
            return BitmapFactory.DecodeStream(res.OpenRawResource(id));
        }
    }
}