using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using AndroidX.Core.Content;
using ImageEntrySampelApp.CustomControl;
using LonginesEquestrianApp.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImageEntry), typeof(ImageEntryRenderer_Droid))]
namespace LonginesEquestrianApp.Droid.CustomRenderer
{
	public class ImageEntryRenderer_Droid : EntryRenderer
	{
		ImageEntry element;

		public ImageEntryRenderer_Droid(Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || e.NewElement == null)
				return;

			element = (ImageEntry)this.Element;
			var editText = this.Control;

			switch (element.ImageAlignment)
			{
				case ImageAlignment.Left:
					if (!string.IsNullOrEmpty(element.LeftImage))
					{
						editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.LeftImage), null, null, null);
					}
					break;

				case ImageAlignment.Right:
					if (!string.IsNullOrEmpty(element.RightImage))
					{
						editText.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(element.RightImage), null);
					}
					break;

				case ImageAlignment.Both:
					if ((!string.IsNullOrEmpty(element.LeftImage)) && (!string.IsNullOrEmpty(element.RightImage)))
					{
						editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.LeftImage), null, GetDrawable(element.RightImage), null);
					}
					break;
			}

			var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RoundRectShape(new float[] { 18, 18, 18, 18, 18, 18, 18, 18 }, null, null));
			shape.Paint.Color = element.LineColor.ToAndroid();
			shape.Paint.StrokeWidth = 3;
			shape.Paint.SetStyle(Paint.Style.Stroke);
			this.Control.Background = shape;
		}

		private BitmapDrawable GetDrawable(string imageEntryImage)
		{
			int resID = Resources.GetIdentifier(System.IO.Path.GetFileNameWithoutExtension(imageEntryImage), "drawable", this.Context.PackageName);
			var drawable = ContextCompat.GetDrawable(this.Context, resID);
			var bitmap = ((BitmapDrawable)drawable).Bitmap;

			return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, element.ImageWidth, element.ImageHeight, true));
		}
	}
}
