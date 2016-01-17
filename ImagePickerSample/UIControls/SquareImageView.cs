using System;
using FFImageLoading.Forms;
using Xamarin.Forms;

namespace ImagePickerSample
{
	public class SquareImageView : CachedImage
	{
		public SquareImageView (double imageSize)
		{
			HorizontalOptions = LayoutOptions.Center;
			VerticalOptions = LayoutOptions.Center;
			WidthRequest = imageSize;
			HeightRequest = imageSize;
			DownsampleToViewSize = true;
			Aspect = Aspect.AspectFit;
		}
	}
}

