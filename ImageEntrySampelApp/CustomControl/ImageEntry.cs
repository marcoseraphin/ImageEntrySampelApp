using System;
using Xamarin.Forms;

namespace ImageEntrySampelApp.CustomControl
{
	public enum ImageAlignment
	{
		Left,
		Right,
		Both,
		None
	}

	public class ImageEntry : Entry
	{
		public ImageEntry()
		{
			this.HeightRequest = 50;
		}

		public static readonly BindableProperty LeftImageProperty =
			BindableProperty.Create(nameof(LeftImage), typeof(string), typeof(ImageEntry), string.Empty);

		public static readonly BindableProperty RightImageProperty =
	        BindableProperty.Create(nameof(RightImage), typeof(string), typeof(ImageEntry), string.Empty);

		public static readonly BindableProperty LineColorProperty =
			BindableProperty.Create(nameof(LineColor), typeof(Xamarin.Forms.Color), typeof(ImageEntry), Color.White);

		public static readonly BindableProperty ImageHeightProperty =
			BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(ImageEntry), 40);

		public static readonly BindableProperty ImageWidthProperty =
			BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(ImageEntry), 40);

		public static readonly BindableProperty ImageAlignmentProperty =
			BindableProperty.Create(nameof(ImageAlignment), typeof(ImageAlignment), typeof(ImageEntry), ImageAlignment.Left);

		public Color LineColor
		{
			get { return (Color)GetValue(LineColorProperty); }
			set { SetValue(LineColorProperty, value); }
		}

		public int ImageWidth
		{
			get { return (int)GetValue(ImageWidthProperty); }
			set { SetValue(ImageWidthProperty, value); }
		}

		public int ImageHeight
		{
			get { return (int)GetValue(ImageHeightProperty); }
			set { SetValue(ImageHeightProperty, value); }
		}

		public string LeftImage
		{
			get { return (string)GetValue(LeftImageProperty); }
			set { SetValue(LeftImageProperty, value); }
		}

		public string RightImage
		{
			get { return (string)GetValue(RightImageProperty); }
			set { SetValue(RightImageProperty, value); }
		}

		public ImageAlignment ImageAlignment
		{
			get { return (ImageAlignment)GetValue(ImageAlignmentProperty); }
			set { SetValue(ImageAlignmentProperty, value); }
		}
	}
}
