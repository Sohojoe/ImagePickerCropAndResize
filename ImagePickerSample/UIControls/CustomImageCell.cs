using System;
using Xamarin.Forms;
using FFImageLoading.Forms;

namespace ImagePickerSample
{
	public class CustomImageCell : ViewCell
	{
		SquareImageView squareImage;
		public CustomImageCell (double imageSize)
		{
			squareImage = new SquareImageView(imageSize);
			View = squareImage;
		}
	}
}

