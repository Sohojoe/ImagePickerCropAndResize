using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using FFImageLoading.Forms;
using System.Collections.Generic;
using FFImageLoading.Work;
using FFImageLoading.Transformations;
using System.IO;

namespace ImagePickerSample
{
	public class PickerPage : ContentPage
	{
		Label status = new Label();
		CachedImage ffImage = new CachedImage{
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
			WidthRequest = 320,
			HeightRequest = 320,
			CacheDuration = TimeSpan.FromDays(30),
			DownsampleToViewSize = false,
			RetryCount = 0,
			RetryDelay = 250,
			TransparencyEnabled = false,
//			LoadingPlaceholder = "loading.png",
//			ErrorPlaceholder = "error.png",
			Source = "http://loremflickr.com/600/600/nature?filename=simple.jpg",
			Aspect = Aspect.AspectFill,
		};
		Image formsImage = new Image{
			WidthRequest = 50,
			Aspect = Aspect.AspectFill,
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
		};
		Image exportedImage = new Image{
			HeightRequest = 50,
			WidthRequest = 50,
			Aspect = Aspect.AspectFill,
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
		};
		Button retake = new Button{
			Text = "retake",	
		};


		public PickerPage ()
		{
			ffImage.Transformations =  new List<ITransformation>() {
				new CropTransformation(1, 0.0, 0.0, 1f, 1f)
			};
			var stack = new StackLayout {
				HorizontalOptions = LayoutOptions.Center,
				Padding = new Thickness(0),
			};
			stack.Children.Add (new Label { Text = " " }); // padding at top
			stack.Children.Add (status);
			stack.Children.Add (ffImage);
			stack.Children.Add (formsImage);
			stack.Children.Add (exportedImage);
			stack.Children.Add (retake);
			status.Text = "init";
			this.Content = stack;
			Device.BeginInvokeOnMainThread (async() => await PickAsync ());
			retake.Command = new Command(async() => await PickAsync ());
		}

		async Task PickAsync()
		{
			var mediaFile = await XamPluginMediaPicker.PickImageAsync ();
			if (mediaFile == null)
				return; // failed
			Debug.WriteLine (mediaFile);
			ffImage.Source = Xamarin.Forms.ImageSource.FromFile (mediaFile.Path);
			formsImage.Source = Xamarin.Forms.ImageSource.FromStream (() => mediaFile.GetStream());
//			ffImage.Source = ImageSource.FromStream (() => imageStream);
//			formsImage.Source = ImageSource.FromStream (() => imageStream);
			ffImage.Finish += FfImage_Finish;
		}

		async void FfImage_Finish (object sender, CachedImageEvents.FinishEventArgs e)
		{
			ffImage.Finish -= FfImage_Finish;

			var asBytes = await ffImage.GetImageAsJpgAsync (90, 480, 480);
			var asStream = new MemoryStream(asBytes);
			exportedImage.Source = Xamarin.Forms.ImageSource.FromStream (() => asStream);
			asBytes = null;

		}


	}
}

