using System;
using System.Drawing;
using System.Threading.Tasks;
using CoreAnimation;
using CoreGraphics;
using ImageEntrySampelApp.CustomControl;
using LonginesEquestrianApp.iOS.CustomRenderer;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamSvg;
using XamSvg.Shared.Cross;
using XamSvg.XamForms;

[assembly: ExportRenderer(typeof(ImageEntry), typeof(ImageEntryRenderer_iOS))]
namespace LonginesEquestrianApp.iOS.CustomRenderer
{
	public class ImageEntryRenderer_iOS : EntryRenderer
	{
		public ImageEntryRenderer_iOS()
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || e.NewElement == null)
				return;

			var element = (ImageEntry)this.Element;
			var textField = this.Control;
			switch (element.ImageAlignment)
			{
				case ImageAlignment.Left:
					if (!string.IsNullOrEmpty(element.LeftImage))
					{
						UIView paddingViewRight = new UIView(new CGRect(8, 0, (float)element.Width - 8, (float)element.Height));
						textField.RightView = paddingViewRight;
						textField.RightViewMode = UITextFieldViewMode.Always;

						textField.LeftViewMode = UITextFieldViewMode.Always;
						textField.LeftView = GetImageView(element.LeftImage, element.ImageHeight, element.ImageWidth);
					}
					break;

				case ImageAlignment.Right:
					if (!string.IsNullOrEmpty(element.RightImage))
					{
						UIView paddingViewLeft = new UIView(new CGRect(8, 0, (float)element.Width - 8, (float)element.Height));
						textField.LeftView = paddingViewLeft;
						textField.LeftViewMode = UITextFieldViewMode.Always;

						textField.RightViewMode = UITextFieldViewMode.Always;
						textField.RightView = GetImageView(element.RightImage, element.ImageHeight, element.ImageWidth);
					}
					break;

				case ImageAlignment.Both:
					if ((!string.IsNullOrEmpty(element.LeftImage)) && (!string.IsNullOrEmpty(element.RightImage)))
					{
						textField.LeftViewMode = UITextFieldViewMode.Always;
						textField.LeftView = GetImageView(element.LeftImage, element.ImageHeight, element.ImageWidth);
						textField.RightViewMode = UITextFieldViewMode.Always;
						textField.RightView = GetImageView(element.RightImage, element.ImageHeight, element.ImageWidth);
					}
					break;

				default:
					UIView paddingView = new UIView(new CGRect(8, 0, (float)element.Width - 8, (float)element.Height));
					textField.LeftView = paddingView;
					textField.LeftViewMode = UITextFieldViewMode.Always;
					textField.RightView = paddingView;
					textField.RightViewMode = UITextFieldViewMode.Always;
					break;
			}

			
			textField.BorderStyle = UITextBorderStyle.Line;
			textField.Layer.BorderColor = element.LineColor.ToCGColor();
			textField.Layer.BackgroundColor = element.BackgroundColor.ToCGColor();
			textField.Layer.BorderWidth = 0.8f;
			textField.Layer.CornerRadius = 6;
		}

		private UIView GetImageView(string imagePath, int height, int width)
		{

			// TODO: Load SVG from resources
			//var svgImageSource = SvgImageSource.FromResource(imagePath);
			//var uiImageView = new UIImageView(await GetUIImageFromImageSourceAsync(svgImageSource))
			//{
			//	Frame = new RectangleF(8, 0, width, height)
			//};


			var uiImageView = new UIImageView(UIImage.FromBundle(imagePath))
			{
				Frame = new RectangleF(8, 0, width, height)
			};

			UIView objLeftView = new UIView(new System.Drawing.Rectangle(0, 0, width + 20, height));
			objLeftView.AddSubview(uiImageView);

			return objLeftView;
		}

		public static async Task<UIImage> GetUIImageFromImageSourceAsync(ImageSource source)
		{
			var handler = GetHandler(source);
			var returnValue = (UIImage)null;

			returnValue = await handler.LoadImageAsync(source);

			return returnValue;
		}

		private static IImageSourceHandler GetHandler(ImageSource source)
		{
			IImageSourceHandler returnValue = null;
			if (source is UriImageSource)
			{
				returnValue = new ImageLoaderSourceHandler();
			}
			else if (source is FileImageSource)
			{
				returnValue = new FileImageSourceHandler();
			}
			else if (source is StreamImageSource)
			{
				returnValue = new StreamImagesourceHandler();
			}
			return returnValue;
		}

		private async Task<UIImage> GetBitmapAsync(ImageSource source)
		{
			var handler = GetHandler(source);
			var returnValue = (UIImage)null;

			returnValue = await handler.LoadImageAsync(source);

			return returnValue;
		}
	} 

	//public static class ImageSourceExtensions
	//{
	//	static ImageLoaderSourceHandler s_imageLoaderSourceHandler;

	//	static ImageSourceExtensions()
	//	{
	//		s_imageLoaderSourceHandler = new ImageLoaderSourceHandler();
	//	}
	//	public static Task<UIImage> ToUIImage(this ImageSource imageSource)
	//	{
	//		return s_imageLoaderSourceHandler.LoadImageAsync(imageSource);
	//	}
	//}
}
